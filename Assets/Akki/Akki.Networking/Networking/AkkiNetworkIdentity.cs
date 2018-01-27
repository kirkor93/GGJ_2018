
namespace Akki.Networking
{
    using UnityEngine;
    using GooglePlayGames.BasicApi.Multiplayer;

    public class AkkiNetworkIdentity : MonoBehaviour, IRoomListener
    {
        #region Variables

        private string a_networkId = null;
        private bool a_isLocalPlayer;
        private AkkiNetworkBehaviour[] a_networkBehaviours;

        #endregion /Variables

        #region Properties

        public string NetworkId
        {
            get { return a_networkId; }
            set { a_networkId = value; }
        }

        public bool IsLocalPlayer
        {
            get { return a_isLocalPlayer; }
            set { a_isLocalPlayer = value; }
        }

        #endregion /Properties

        #region UnityCallbacks

        void Awake()
        {
            CachedBehaviours();
        }

        #endregion

        #region IRoomListener

        public void OnPlayerLeftRoom()
        {
            CachedBehaviours();
            for (int i = 0; i < a_networkBehaviours.Length; i++)
            {
                a_networkBehaviours[i].OnPlayerLeftRoom();
            }
        }

        public void OnConnectedRoom(string id)
        {
            CachedBehaviours();
            for (int i = 0; i < a_networkBehaviours.Length; i++)
            {
                a_networkBehaviours[i].OnConnectedRoom(id);
            }
        }

        public void OnDisconnect(string id)
        {
            CachedBehaviours();
            for (int i = 0; i < a_networkBehaviours.Length; i++)
            {
                a_networkBehaviours[i].OnDisconnect(id);
            }
        }

        public void OnOtherPlayerLeftRoom(Participant otherPlayer)
        {
            CachedBehaviours();
            for (int i = 0; i < a_networkBehaviours.Length; i++)
            {
                a_networkBehaviours[i].OnOtherPlayerLeftRoom(otherPlayer);
            }
        }

        public void OnMessageReceived(string senderId, byte[] data)
        {
            CachedBehaviours();
            Debug.Log("Message Received with Identity..." + senderId);
            for (int i = 0; i < a_networkBehaviours.Length; i++)
            {
                a_networkBehaviours[i].OnMessageReceived(senderId, data);
            }
        }

        public void OnMessageSend()
        {
            //Handle By AkkiNetworkBehaviour Child Class.
        }

        #endregion

        #region Utility

        internal void CachedBehaviours()
        {
            if (a_networkBehaviours == null)
            {
                a_networkBehaviours = GetComponents<AkkiNetworkBehaviour>();
                Debug.Log("Found Network behaviours : " + a_networkBehaviours.Length);
            }
        }

        #endregion
    }
}
