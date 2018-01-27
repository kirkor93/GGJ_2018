using UnityEngine;

namespace Akki
{
    using GooglePlayGames;
    using GooglePlayGames.BasicApi.Multiplayer;

    public class AkkiRtmRoomManager
    {
        private AkkiRtmInvitationManager a_Invitation = new AkkiRtmInvitationManager();

        public AkkiRtmInvitationManager InvitationManager
        {
            get { return a_Invitation; }
        }

        private AkkiRtmParticipant a_Participant = new AkkiRtmParticipant();

        public AkkiRtmParticipant Participant
        {
            get { return a_Participant; }
        }

        private RealTimeMultiplayerListener a_listener = null;

        public RealTimeMultiplayerListener Listener
        {
            get { return a_listener; }
            set { a_listener = value; }
        }

        const uint QuickGameOpponents = 1;
        const uint MinOpponents = 1;
        const uint MaxOpponents = 8;

        public uint GameVariant = 0;

        public bool IsRoomConnected
        {
            get { return PlayGamesPlatform.Instance.RealTime.IsRoomConnected(); }
        }

        #region RoomHandling

        public void CreateQuickGame()
        {
            PlayGamesPlatform.Instance.RealTime.CreateQuickGame(QuickGameOpponents, QuickGameOpponents, GameVariant, Listener);
        }

        public void CreateWithInvitationScreen()
        {
            PlayGamesPlatform.Instance.RealTime.CreateWithInvitationScreen(MinOpponents, MaxOpponents, GameVariant, Listener);
        }

        public void AcceptFromInbox()
        {
            PlayGamesPlatform.Instance.RealTime.AcceptFromInbox(Listener);
        }

        public void AcceptInvitation(string invitationId)
        {
            PlayGamesPlatform.Instance.RealTime.AcceptInvitation(invitationId, Listener);
        }

        public void ShowWaitingRoomUi()
        {
            PlayGamesPlatform.Instance.RealTime.ShowWaitingRoomUI();
        }

        public void LeaveRoom()
        {
            if (IsRoomConnected)
            {
                PlayGamesPlatform.Instance.RealTime.LeaveRoom();
            }
        }

        #endregion /RoomHandling

        #region MessageSender

        /// <summary>
        /// Send Message to all connected participants.
        /// You are sending a broadcast message, make sure to exclude the sender participant from the list of broadcast recipients.
        /// </summary>
        /// <remarks>
        /// The maximum size of a reliable message that you can send is 1400 bytes.
        /// </remarks>
        /// <param name="reliable"></param>
        /// <param name="data"></param>
        public void SendMessageToAll(bool reliable, byte[] data)
        {
            Debug.Log("Sending message from Room Manager");
            PlayGamesPlatform.Instance.RealTime.SendMessageToAll(reliable, data);
        }

        /// <summary>
        /// Send Message to perticular connected participant.
        /// </summary>
        /// <remarks>
        /// The maximum size for an unreliable message that you can send is 1168 bytes.
        /// </remarks>
        /// <param name="reliable"></param>
        /// <param name="participantId"></param>
        /// <param name="data"></param>
        public void SendMessage(bool reliable, string participantId, byte[] data)
        {
            PlayGamesPlatform.Instance.RealTime.SendMessage(reliable, participantId, data);
        }

        #endregion /MessageSender
    }
}
