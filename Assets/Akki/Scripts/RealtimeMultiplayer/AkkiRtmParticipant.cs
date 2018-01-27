
using UnityEngine;

namespace Akki
{
    using GooglePlayGames;
    using GooglePlayGames.BasicApi.Multiplayer;
    using System.Collections.Generic;

    public class AkkiRtmParticipant
    {
        /// <summary>
        /// Get Self Participant.
        /// </summary>
        /// <returns>self</returns>
        public Participant GetSelfParticipant()
        {
            return PlayGamesPlatform.Instance.RealTime.GetSelf();
        }

        /// <summary>
        /// Get All Participants.
        /// </summary>
        /// <returns>All Participant.</returns>
        public List<Participant> GetAllParticipants()
        {
            return PlayGamesPlatform.Instance.RealTime.GetConnectedParticipants();
        }

        /// <summary>
        /// Get Participant by its Id.
        /// </summary>
        /// <param name="participantId"></param>
        /// <returns>participant</returns>
        public Participant GetParticipantById(string participantId)
        {
            return PlayGamesPlatform.Instance.RealTime.GetParticipant(participantId);
        }

        /// <summary>
        /// Get self Participant Id.
        /// </summary>
        /// <returns></returns>
        public string GetSelfParticipantId()
        {
            Debug.Log("Return Patricipant Id.");
            return PlayGamesPlatform.Instance.RealTime.GetSelf().ParticipantId;
        }
    }
}
