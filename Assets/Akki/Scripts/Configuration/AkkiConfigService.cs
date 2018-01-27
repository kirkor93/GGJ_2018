namespace Akki
{
    using GooglePlayGames;
    using GooglePlayGames.BasicApi;
    using GooglePlayGames.BasicApi.Multiplayer;

    public class AkkiConfigService
    {
        private bool a_IsSavegame;
        private InvitationReceivedDelegate a_InvitationReceivedDelegate;
        private MatchDelegate a_MatchDelegate;
        private bool a_IsRequestedEmail;
        private bool a_IsRequestedServerAuthCode;
        private bool a_IsRequestedIdToken;

        public bool IsSavegame
        {
            get
            {
                return a_IsSavegame;
            }
        }

        public bool HasInvitationReceivedDelegate
        {
            get { return a_InvitationReceivedDelegate != null; }
        }

        public bool HasMatchDelegate
        {
            get { return a_MatchDelegate != null; }
        }

        public bool IsRequestEmail
        {
            get { return a_IsRequestedEmail; }
        }

        public bool IsRequestServerAuthCode
        {
            get { return a_IsRequestedServerAuthCode; }
        }

        public bool IsRequestIdToken
        {
            get { return a_IsRequestedIdToken; }
        }

        #region Configuration

        public void Configure()
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();

            PlayGamesPlatform.InitializeInstance(config);
            // recommended for debugging:
            PlayGamesPlatform.DebugLogEnabled = true;
            // Activate the Google Play Games platform
            PlayGamesPlatform.Activate();
        }

        /// <summary>
        /// Configure your play game service.
        /// </summary>
        /// <param name="savegame"></param>
        /// <param name="invitationDelegate"></param>
        /// <param name="matchDelegate"></param>
        /// <param name="requestEmail"></param>
        /// <param name="requestServerAuthCode"></param>
        /// <param name="requestIdToken"></param>
        /// <param name="debugLogEnable"></param>
        public void Configure(
            bool savegame = false,
            InvitationReceivedDelegate invitationDelegate = null,
            MatchDelegate matchDelegate = null,
            bool requestEmail = false,
            bool requestServerAuthCode = false,
            bool requestIdToken = false,
            bool debugLogEnable = true)
        {
            PlayGamesClientConfiguration.Builder builder = new PlayGamesClientConfiguration.Builder();

            if (savegame)
            {
                // enables saving game progress.
                builder.EnableSavedGames();
                a_IsSavegame = true;
            }

            if (invitationDelegate != null)
            {
                // registers a callback to handle game invitations received while the game is not running.
                builder.WithInvitationDelegate(invitationDelegate);
                a_InvitationReceivedDelegate = invitationDelegate;
            }

            if (matchDelegate != null)
            {
                // registers a callback for turn based match notifications received while the
                // game is not running.
                builder.WithMatchDelegate(matchDelegate);
                a_MatchDelegate = matchDelegate;
            }

            if (requestEmail)
            {
                // requests the email address of the player be available.
                // Will bring up a prompt for consent.
                builder.RequestEmail();
                a_IsRequestedEmail = true;
            }

            if (requestServerAuthCode)
            {
                // requests a server auth code be generated so it can be passed to an
                //  associated back end server application and exchanged for an OAuth token.
                builder.RequestServerAuthCode(false);
                a_IsRequestedServerAuthCode = true;
            }

            if (requestIdToken)
            {
                // requests an ID token be generated.  This OAuth token can be used to
                //  identify the player to other services such as Firebase.
                builder.RequestIdToken();
                a_IsRequestedIdToken = true;
            }

            PlayGamesClientConfiguration config = builder.Build();

            PlayGamesPlatform.InitializeInstance(config);
            // recommended for debugging:
            PlayGamesPlatform.DebugLogEnabled = debugLogEnable;
            // Activate the Google Play Games platform
            PlayGamesPlatform.Activate();
        }
        #endregion /Configuration
    }
}
