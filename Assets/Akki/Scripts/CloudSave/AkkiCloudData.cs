namespace Akki
{
    using System;
    using GooglePlayGames;
    using GooglePlayGames.BasicApi;
    using GooglePlayGames.BasicApi.SavedGame;
    using UnityEngine;

    public class AkkiCloudData
    {
        // keep track of saving or loading during callbacks.
        private bool a_Saving;

        // save your game screenshot as progress image in cloud.
        private Texture2D a_ScreenImage;

        // cache data that you want to save in cloud.
        private string a_dataToSave;

        private Action<string> LoadData;

        private void OnDataLoaded(string data)
        {
            if (LoadData != null)
            {
                LoadData.Invoke(data);
            }
        }

        /// <summary>
        /// Load Data From Cloud.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="onDataLoaded"></param>
        public void LoadDataFromCloud(string filename, Action<string> onDataLoaded)
        {
            // Cloud save is not in ISocialPlatform, it's a Play Games extension,
            // so we have to break the abstraction and use PlayGamesPlatform:
            Debug.Log("Loading game progress from the cloud.");
            a_Saving = false;

            if (onDataLoaded != null)
            {
                LoadData = onDataLoaded;
            }

            if (filename == null)
            {
                ((PlayGamesPlatform)Social.Active).SavedGame.ShowSelectSavedGameUI("Select saved game to load",
                    4, false, false, SavedGameSelected);
            }
            else
            {
                // Load From named file
                ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution(filename,
                    DataSource.ReadCacheOrNetwork,
                    ConflictResolutionStrategy.UseLongestPlaytime,
                    SavedGameOpened);
            }
        }

        /// <summary>
        /// Save Data to cloud.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="datatosave"></param>
        public void SaveDataToCloud(string filename, string datatosave)
        {
            if (Social.Active.localUser.authenticated)
            {
                // Cloud save is not in ISocialPlatform, it's a Play Games extension,
                // so we have to break the abstraction and use PlayGamesPlatform:
                Debug.Log("Saving progress to the cloud...");
                a_Saving = true;
                // Save data locally for later use.
                if (!string.IsNullOrEmpty(datatosave))
                {
                    a_dataToSave = datatosave;
                }

                if (filename == null)
                {
                    ((PlayGamesPlatform)Social.Active).SavedGame.ShowSelectSavedGameUI("Save game progress",
                        4, true, true, SavedGameSelected);
                }
                else
                {
                    // save to named file
                    ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution(filename,
                        DataSource.ReadCacheOrNetwork,
                        ConflictResolutionStrategy.UseLongestPlaytime,
                        SavedGameOpened);
                }
            }
        }

        private void SavedGameSelected(SelectUIStatus status, ISavedGameMetadata game)
        {
            if (status == SelectUIStatus.SavedGameSelected)
            {
                string filename = game.Filename;
                Debug.Log("opening saved game:  " + game);
                //open the data.
                ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution(filename,
                    DataSource.ReadCacheOrNetwork,
                    ConflictResolutionStrategy.UseLongestPlaytime,
                    SavedGameOpened);
            }
            else
            {
                Debug.LogWarning("Error selecting save game: " + status);
            }

        }

        private void SavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
        {
            if (status == SavedGameRequestStatus.Success)
            {
                if (a_Saving)
                {
                    WriteGameToCloud(game);
                }
                else
                {
                    ReadGameFromCloud(game);
                }
            }
            else
            {
                Debug.LogWarning("Error opening game: " + status);
            }
        }

        private void ReadGameFromCloud(ISavedGameMetadata game)
        {
            ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(game, SavedGameLoaded);
        }

        private void WriteGameToCloud(ISavedGameMetadata game)
        {
            if (a_ScreenImage == null)
            {
                CaptureScreenshot();
            }
            byte[] pngData = (a_ScreenImage != null) ? a_ScreenImage.EncodeToPNG() : null;
            Debug.Log("Saving to " + game);
            byte[] data = GetBytes(a_dataToSave);
            TimeSpan playedTime = TotalPlayedTime();

            SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder()
                .WithUpdatedPlayedTime(playedTime)
                .WithUpdatedDescription("Saved Game at " + DateTime.Now);

            if (pngData != null)
            {
                Debug.Log("Save image of len " + pngData.Length);
                builder = builder.WithUpdatedPngCoverImage(pngData);
            }
            else
            {
                Debug.Log("No image avail");
            }
            SavedGameMetadataUpdate updatedMetadata = builder.Build();
            ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(game, updatedMetadata, data, SavedGameWritten);
        }

        private void SavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
        {
            if (status == SavedGameRequestStatus.Success)
            {
                Debug.Log("Game " + game.Description + " written");
            }
            else
            {
                Debug.LogWarning("Error saving game: " + status);
            }
        }

        private void SavedGameLoaded(SavedGameRequestStatus status, byte[] data)
        {
            if (status == SavedGameRequestStatus.Success)
            {
                Debug.Log("SaveGameLoaded, success=" + status);
                // Now you can save cloud data locally.
                LoadLocalData(GetString(data));
            }
            else
            {
                Debug.LogWarning("Error reading game: " + status);
            }
        }

        private void LoadLocalData(string data)
        {
            // Save last played game progress locally.
            OnDataLoaded(data);
        }

        #region Utility
        /// <summary>
        /// Capture screen shot.
        /// </summary>
        private void CaptureScreenshot()
        {
            a_ScreenImage = new Texture2D(Screen.width, Screen.height);
            a_ScreenImage.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            a_ScreenImage.Apply();
            Debug.Log("Captured screen: " + a_ScreenImage);
        }

        private byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        private string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        private TimeSpan TotalPlayedTime()
        {
            DateTime totalSpan = new DateTime(0, 0, 0, 0, 0, (int)Time.realtimeSinceStartup);
            return DateTime.Now.Subtract(totalSpan);
        }

        #endregion /Utility

    }
}
