namespace Akki
{
    using GooglePlayGames;
    using System.Collections.Generic;
    using GooglePlayGames.BasicApi.Multiplayer;

    public class AkkiRtmInvitationManager
    {
        private List<Invitation> InvitationStoragesList = new List<Invitation>();

        public void OnInvitationReceived(Invitation inv, bool shouldAutoAccept)
        {
            if (shouldAutoAccept)
            {
                // Invitation should be accepted immediately. This happens if the user already
                // indicated (through the notification UI) that they wish to accept the invitation,
                // so we should not prompt again.
                AkkiGpgsManager.Instance.RealTimeRoomManager.AcceptInvitation(inv.InvitationId);
            }
            else
            {
                // The user has not yet indicated that they want to accept this invitation.
                // We should *not* automatically accept it. Rather we store it and 
                // display an in-game popup:
                if (!InvitationStoragesList.Contains(inv))
                {
                    InvitationStoragesList.Add(inv);
                }
            }
        }

        public void DeclineInvitation(string invitaionId)
        {
            for (int i = 0; i < InvitationStoragesList.Count; i++)
            {
                if (InvitationStoragesList[i].InvitationId == invitaionId)
                {
                    InvitationStoragesList.Remove(InvitationStoragesList[i]);
                    break;
                }
            }

            PlayGamesPlatform.Instance.RealTime.DeclineInvitation(invitaionId);
        }

        public void Clear()
        {
            InvitationStoragesList.Clear();
        }

        public List<Invitation> GetPendingInvitations()
        {
            return InvitationStoragesList;
        }

        public List<Invitation> GetAllInvitations()
        {
            List<Invitation> allInvitations = new List<Invitation>();
            PlayGamesPlatform.Instance.RealTime.GetAllInvitations(
                (invites) =>
                {
                    foreach (Invitation t in invites)
                    {
                        allInvitations.Add(t);
                    }
                });
            return allInvitations;
        }
    }
}

