
namespace Akki
{
    using System;
    using UnityEngine;
    using GooglePlayGames;

    public class AkkiAchievement
    {
        /// <summary>
        /// Reveal an achievement (that was previously hidden) without unlocking it.
        /// </summary>
        /// <param name="achievementId"></param>
        /// <param name="callback"></param>
        public void RevealAchievement(string achievementId, Action<bool> callback)
        {
            Social.ReportProgress(achievementId, 0.0f, callback);
        }

        /// <summary>
        /// A progress of 100.0f means unlocking the achievement.
        /// </summary>
        /// <param name="achievementId"></param>
        /// <param name="callback"></param>
        public void UnlockAchievement(string achievementId, Action<bool> callback)
        {
            Social.ReportProgress(achievementId, 100.0f, callback);
        }

        /// <summary>
        /// Incrementing an Achievement.
        /// </summary>
        /// <param name="achievementId"></param>
        /// <param name="steps"></param>
        /// <param name="callback"></param>
        public void IncrementalAchievement(string achievementId, int steps, Action<bool> callback)
        {
            PlayGamesPlatform.Instance.IncrementAchievement(achievementId, steps, callback);
        }

        /// <summary>
        /// Show Achievement Ui.
        /// </summary>
        public void ShowAchievementUi()
        {
            if (Social.Active.localUser.authenticated)
            {
                Social.ShowAchievementsUI();
            }
        }
    }
}
