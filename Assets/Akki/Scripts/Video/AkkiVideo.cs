

namespace Akki
{
    using GooglePlayGames;
    using GooglePlayGames.BasicApi;
    using GooglePlayGames.BasicApi.Video;
    using UnityEngine;

    public class AkkiVideo : CaptureOverlayStateListener
    {

        public void RegisterListener()
        {
            PlayGamesPlatform.Instance.Video.RegisterCaptureOverlayStateChangedListener(this);
        }

        public void UnRegisterListener()
        {
            PlayGamesPlatform.Instance.Video.UnregisterCaptureOverlayStateChangedListener();
        }

        private void DeviceHasVideoCaptureCapabilities()
        {
            PlayGamesPlatform.Instance.Video.GetCaptureCapabilities(
                (status, capabilities) =>
                {
                    bool isSuccess = CommonTypesUtil.StatusIsSuccess(status);
                    if (isSuccess)
                    {
                        if (capabilities.IsCameraSupported && capabilities.IsMicSupported &&
                            capabilities.IsWriteStorageSupported &&
                            capabilities.SupportsCaptureMode(VideoCaptureMode.File) &&
                            capabilities.SupportsQualityLevel(VideoQualityLevel.SD))
                        {
                            Debug.Log("All requested capabilities are present.");
                            CaptureAvailable();
                        }
                        else
                        {
                            Debug.Log("Not all requested capabilities are present!");
                        }
                    }
                    else
                    {
                        Debug.Log("Error: " + status.ToString());
                    }
                });
        }

        /// <summary>
        /// Starting Up the video Recording process.
        /// </summary>
        public void ShowVideoCaptureOverlay()
        {
            if (IsCaptureSupported())
            {
                DeviceHasVideoCaptureCapabilities();
            }
            else
            {
                Debug.LogError("Video Capture not suppored.");
            }
        }

        public void GetVideoCaptureState()
        {
            PlayGamesPlatform.Instance.Video.GetCaptureState(
                (status, state) =>
                {
                    bool isSuccess = CommonTypesUtil.StatusIsSuccess(status);
                    if (isSuccess)
                    {
                        if (state.IsCapturing)
                        {
                            Debug.Log("Currently capturing to " + state.CaptureMode.ToString() + " in " +
                                      state.QualityLevel.ToString());
                        }
                        else
                        {
                            Debug.Log("Not currently capturing.");
                        }
                    }
                    else
                    {
                        Debug.Log("Error: " + status.ToString());
                    }
                });
        }

        private bool IsCaptureSupported()
        {
            return PlayGamesPlatform.Instance.Video.IsCaptureSupported();
        }

        private void CaptureAvailable()
        {
            PlayGamesPlatform.Instance.Video.IsCaptureAvailable(VideoCaptureMode.File,
                (status, isAvailable) =>
                {
                    bool isSuccess = CommonTypesUtil.StatusIsSuccess(status);
                    if (isSuccess)
                    {
                        if (isAvailable)
                        {
                            PlayGamesPlatform.Instance.Video.ShowCaptureOverlay();
                            Debug.Log("Capture is available.");
                        }
                        else
                        {
                            Debug.Log("Video capture is unavailable. Is the overlay already open?");
                        }
                    }
                    else
                    {
                        Debug.Log("Error: " + status.ToString());
                    }
                });
        }

        #region CaptureOverlayStateListenerCallback

        public void OnCaptureOverlayStateChanged(VideoCaptureOverlayState overlayState)
        {
            Debug.Log("Overlay State is now " + overlayState.ToString());
        }

        #endregion /CaptureOverlayStateListenerCallback

    }
}
