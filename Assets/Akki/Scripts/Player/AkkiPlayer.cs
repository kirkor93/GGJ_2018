using UnityEngine.SocialPlatforms;

namespace Akki
{
    using UnityEngine;
    using GooglePlayGames;

    public class AkkiPlayer
    {
        public string GetPlayerName()
        {
            return Social.localUser.userName;
        }

        public string GetPlayerId()
        {
            return Social.localUser.id;
        }

        public string GetPlayerEmailAddress()
        {
            return PlayGamesPlatform.Instance.GetUserEmail();
        }

        public Texture2D GetPlayerImage()
        {
            return Social.localUser.image;
        }

        public IUserProfile[] GetPlayerFriends()
        {
            IUserProfile[] friends = null;
            Social.localUser.LoadFriends((ok) =>
            {
                if (ok)
                {
                    friends = Social.localUser.friends;
                }
            });
            return friends;
        }
    }
}