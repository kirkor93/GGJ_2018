
using System;

namespace Akki
{
    using GooglePlayGames;
    using UnityEngine;

    public class AkkiLeaderboard
    {
        /// <summary>
        /// Show all Leaderboard in google standard UI.
        /// </summary>
        public void ShowLeaderboardUi()
        {
            if (Social.Active.localUser.authenticated)
            {
                Social.ShowLeaderboardUI();
            }
        }

        /// <summary>
        /// Show specific leaderboard with it's Id.
        /// </summary>
        /// <param name="leaderboardId"></param>
        public void ShowLeaderboardUi(string leaderboardId)
        {
            if (Social.Active.localUser.authenticated && !string.IsNullOrEmpty(leaderboardId))
            {
                PlayGamesPlatform.Instance.ShowLeaderboardUI(leaderboardId);
            }
        }

        /// <summary>
        /// Post a player score to leaderboard.
        /// </summary>
        /// <param name="score"></param>
        /// <param name="leaderboardId"></param>
        /// <param name="callback"></param>
        public void PostLeaderboardScore(long score, string leaderboardId, Action<bool> callback)
        {
            Social.ReportScore(score, leaderboardId, callback);
        }

        /// <summary>
        /// Post a score with metadata.
        /// </summary>
        /// <param name="score"></param>
        /// <param name="leaderboardId"></param>
        /// <param name="metadataTag"></param>
        /// <param name="callback"></param>
        public void PostLeaderboardScore(long score, string leaderboardId, string metadataTag, Action<bool> callback)
        {
            PlayGamesPlatform.Instance.ReportScore(score, leaderboardId, metadataTag, callback);
        }
    }
}
