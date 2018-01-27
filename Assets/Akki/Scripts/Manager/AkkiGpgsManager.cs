
namespace Akki
{
    using System;
    using System.Collections.Generic;
    using GooglePlayGames.BasicApi;
    using GooglePlayGames.BasicApi.Multiplayer;
    using UnityEngine;
    using UnityEngine.SocialPlatforms;

    public class AkkiGpgsManager
    {
        #region Initialization

        private static AkkiGpgsManager a_Instance = new AkkiGpgsManager();

        public static AkkiGpgsManager Instance
        {
            get
            {
                return a_Instance;
            }
        }

        private AkkiConfigService a_ConfigeService = new AkkiConfigService();

        internal AkkiConfigService ConfigurationService
        {
            get { return a_ConfigeService; }
        }

        private AkkiSignUp a_SignUp = new AkkiSignUp();

        internal AkkiSignUp SignUp
        {
            get { return a_SignUp; }
        }

        private AkkiAchievement a_Achievement = new AkkiAchievement();

        internal AkkiAchievement Achievement
        {
            get { return a_Achievement; }
        }

        private AkkiCloudData a_CloudData = new AkkiCloudData();

        internal AkkiCloudData CloudData
        {
            get { return a_CloudData; }
        }

        private AkkiLeaderboard a_Leaderboard = new AkkiLeaderboard();

        internal AkkiLeaderboard Leaderboard
        {
            get { return a_Leaderboard; }
        }

        private AkkiPlayer a_Player = new AkkiPlayer();

        internal AkkiPlayer Player
        {
            get { return a_Player; }
        }

        private AkkiRtmRoomManager a_RtmManager = new AkkiRtmRoomManager();

        internal AkkiRtmRoomManager RealTimeRoomManager
        {
            get { return a_RtmManager; }
        }

        private AkkiVideo a_AkkiVideo = new AkkiVideo();

        internal AkkiVideo Video
        {
            get { return a_AkkiVideo; }
        }

        #endregion /Initialization

        #region Configuration

        public void Configure()
        {
            ConfigurationService.Configure();
        }

        public void Configure(
            bool savegame = false,
            InvitationReceivedDelegate invitationDelegate = null,
            MatchDelegate matchDelegate = null,
            bool requestEmail = false,
            bool requestServerAuthCode = false,
            bool requestIdToken = false,
            bool debugLogEnable = true)
        {
            ConfigurationService.Configure(savegame, invitationDelegate, matchDelegate, requestEmail, requestServerAuthCode,
               requestIdToken, debugLogEnable);
        }

        #endregion /Configuration

        #region SignUp

        public bool IsAutheticated()
        {
            return SignUp.IsAuthenticated;
        }

        public void SignIn(Action onSuccess = null, Action onFailure = null)
        {
            SignUp.SignIn(onSuccess, onFailure);
        }

        public void SignOut()
        {
            SignUp.SignOut();
        }

        public void SetPopUpGravity(Gravity gravity)
        {
            SignUp.SetupPopupGravity(gravity);
        }

        #endregion /SignUp

        #region Achievement

        public void RevealAchievement(string achievementId, Action<bool> callback)
        {
            Achievement.RevealAchievement(achievementId, callback);
        }

        public void UnlockAchievement(string achievementId, Action<bool> callback)
        {
            Achievement.UnlockAchievement(achievementId, callback);
        }

        public void IncrementalAchievement(string achievementId, int steps, Action<bool> callback)
        {
            Achievement.IncrementalAchievement(achievementId, steps, callback);
        }

        public void ShowAchievementUi()
        {
            Achievement.ShowAchievementUi();
        }

        #endregion /Achievement

        #region CloudData

        public void LoadDataFromCloud(string filename, Action<string> onDataLoaded)
        {
            CloudData.LoadDataFromCloud(filename, onDataLoaded);
        }

        public void SaveDataToCloud(string filename, string datatosave)
        {
            CloudData.SaveDataToCloud(filename, datatosave);
        }

        #endregion /CloudData

        #region Leaderboard

        public void ShowLeaderboardUi()
        {
            Leaderboard.ShowLeaderboardUi();
        }

        public void ShowLeaderboardUi(string leaderboardId)
        {
            Leaderboard.ShowLeaderboardUi(leaderboardId);
        }

        public void PostLeaderboardScore(long score, string leaderboardId, Action<bool> callback)
        {
            Leaderboard.PostLeaderboardScore(score, leaderboardId, callback);
        }

        public void PostLeaderboardScore(long score, string leaderboardId, string metadataTag, Action<bool> callback)
        {
            Leaderboard.PostLeaderboardScore(score, leaderboardId, metadataTag, callback);
        }

        #endregion /Leaderboard

        #region PlayerInfo

        public string GetPlayerName()
        {
            return Player.GetPlayerName();
        }

        public string GetPlayerId()
        {
            return Player.GetPlayerId();
        }

        public string GetPlayerEmailAddress()
        {
            return Player.GetPlayerEmailAddress();
        }

        public Texture2D GetPlayerImage()
        {
            return Player.GetPlayerImage();
        }

        public IUserProfile[] GetPlayerFriends()
        {
            return Player.GetPlayerFriends();
        }

        #endregion /PlayerInfo

        #region Video

        public void RegisterVideoStateChangeListener()
        {
            Video.RegisterListener();
        }

        public void UnRegisterVideoStateChangeListener()
        {
            Video.UnRegisterListener();
        }

        public void ShowVideoCaptureOverlay()
        {
            Video.ShowVideoCaptureOverlay();
        }

        #endregion /Video

        #region RealTimeMultiplayer

        public bool IsRoomConnected()
        {
            return RealTimeRoomManager.IsRoomConnected;
        }

        public void CreateQuickGame()
        {
            RealTimeRoomManager.CreateQuickGame();
        }

        public void CreateWithInvitationScreen()
        {
            RealTimeRoomManager.CreateWithInvitationScreen();
        }

        public void AcceptFromInbox()
        {
            RealTimeRoomManager.AcceptFromInbox();
        }

        public void AcceptInvitation(string invitationId)
        {
            RealTimeRoomManager.AcceptInvitation(invitationId);
        }

        public void ShowWaitingRoomUi()
        {
            RealTimeRoomManager.ShowWaitingRoomUi();
        }

        public void LeaveRoom()
        {
            RealTimeRoomManager.LeaveRoom();
        }

        #endregion /RealTimeMultiplayer

        #region Invitation

        public List<Invitation> GetPendingInvitations()
        {
            return RealTimeRoomManager.InvitationManager.GetPendingInvitations();
        }

        public List<Invitation> GetAllInvitations()
        {
            return RealTimeRoomManager.InvitationManager.GetAllInvitations();
        }

        public void DeclineInvitation(string invitaionId)
        {
            RealTimeRoomManager.InvitationManager.DeclineInvitation(invitaionId);
        }

        public void ClearPendingInvitation()
        {
            RealTimeRoomManager.InvitationManager.Clear();
        }

        #endregion /Invitation

        #region Participant

        public Participant GetSelfParticipant()
        {
            return RealTimeRoomManager.Participant.GetSelfParticipant();
        }

        public List<Participant> GetParticipants()
        {
            return RealTimeRoomManager.Participant.GetAllParticipants();
        }

        public Participant GetParticipant(string participantId)
        {
            return RealTimeRoomManager.Participant.GetParticipantById(participantId);
        }

        public string GetSelfParticipantId()
        {
            Debug.Log("Get Self Id");
            return RealTimeRoomManager.Participant.GetSelfParticipantId();
        }

        #endregion /Participant

        #region MessageSender

        public void SendMessageToAll(bool reliable, byte[] data)
        {
            RealTimeRoomManager.SendMessageToAll(reliable, data);
        }

        public void SendMessage(bool reliable, string participantId, byte[] data)
        {
            RealTimeRoomManager.SendMessage(reliable, participantId, data);
        }

        #endregion
    }
}
