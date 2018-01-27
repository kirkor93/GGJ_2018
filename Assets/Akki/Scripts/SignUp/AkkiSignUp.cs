namespace Akki
{
    using System;
    using UnityEngine;
    using GooglePlayGames;
    using GooglePlayGames.BasicApi;

    public class AkkiSignUp
    {
        public bool IsAuthenticated
        {
            get
            {
                return Social.Active.localUser.authenticated;
            }
        }

        /// <summary>
        /// SignIn into google play game service API.
        /// </summary>
        /// <param name="onSuccess"></param>
        /// <param name="onFailure"></param>
        public void SignIn(Action onSuccess = null, Action onFailure = null)
        {
            // authenticate user:
            Social.localUser.Authenticate((bool success) =>
            {
                // handle success or failure
                if (success)
                {
                    // if we signed in successfully, load data from cloud
                    Debug.Log("Login successful!");
                    if (onSuccess != null)
                    {
                        onSuccess.Invoke();
                    }
                }
                else
                {
                    // no need to show error message (error messages are shown automatically
                    // by plugin)
                    Debug.Log("Failed to sign in with Google Play Games.");
                    if (onFailure != null)
                    {
                        onFailure.Invoke();
                    }
                }
            });
        }

        /// <summary>
        /// SignOut from google play games service API.
        /// </summary>
        public void SignOut()
        {
            ((PlayGamesPlatform)Social.Active).SignOut();
        }

        /// <summary>
        /// Setup popup gravity after succesfully signin.
        /// </summary>
        /// <param name="gravity"></param>
        public void SetupPopupGravity(Gravity gravity)
        {
            if (IsAuthenticated)
            {
                ((PlayGamesPlatform)Social.Active).SetGravityForPopups(gravity);
            }
        }
    }
}
