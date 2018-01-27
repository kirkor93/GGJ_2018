using GooglePlayGames.BasicApi.Multiplayer;

namespace Akki.Networking
{
    using UnityEngine;

    public class AkkiNetworkBehaviour : MonoBehaviour, IRoomListener
    {
        private AkkiNetworkIdentity identity = null;
        private string a_networkId = null;
        private bool a_isLocalPlayer;

        public string NetworkId
        {
            get
            {
                if (identity == null)
                {
                    identity = GetComponent<AkkiNetworkIdentity>();
                    a_networkId = identity.NetworkId;
                    a_isLocalPlayer = identity.IsLocalPlayer;
                    return a_networkId;
                }
                return a_networkId;
            }
        }

        public bool IsLocalPlayer
        {
            get
            {
                if (identity == null)
                {
                    identity = GetComponent<AkkiNetworkIdentity>();
                    a_networkId = identity.NetworkId;
                    a_isLocalPlayer = identity.IsLocalPlayer;
                    return a_isLocalPlayer;
                }
                return a_isLocalPlayer;
            }
        }

        public virtual void OnPlayerLeftRoom()
        {
        }

        public virtual void OnConnectedRoom(string id)
        {
        }

        public virtual void OnDisconnect(string id)
        {
        }

        public virtual void OnOtherPlayerLeftRoom(Participant otherPlayerId)
        {
        }

        public virtual void OnMessageReceived(string senderId, byte[] data)
        {

        }

        public virtual void OnMessageSend()
        {

        }
    }
}

