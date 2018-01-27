namespace Akki.Networking
{
    using System;
    using UnityEngine;
    using System.Collections.Generic;
    using GooglePlayGames.BasicApi.Multiplayer;

    public class AkkiNetworkManager : MonoBehaviour, RealTimeMultiplayerListener
    {
        #region Variables

        public static AkkiNetworkManager Instance;

        [Header("Network Player Registration")]
        [Tooltip("Player Object")]
        public GameObject Player;
        [Tooltip("Network Objects other than Player Object")]
        public List<GameObject> RegisterNetworkObjects = new List<GameObject>();


        public Action<bool> RoomConnected;
        public Action<float> RoomSetUp;


        Dictionary<AkkiNetworkIdentity, GameObject> NetworkIdObjectDictionary = new Dictionary<AkkiNetworkIdentity, GameObject>();
        Dictionary<string, AkkiNetworkIdentity> NetworkIdIdDictionary = new Dictionary<string, AkkiNetworkIdentity>();
        /// <summary>
        /// Used for calling all networkbehaviours Calls.
        /// </summary>
        List<AkkiNetworkIdentity> AllAkkiNetworkIdentities = new List<AkkiNetworkIdentity>();

        #endregion /Variables

        #region UnityCallbacks

        void Awake()
        {
            DontDestroyOnLoad(this);

            if (Instance == null)
            {
                Instance = this;
            }

            if (Player != null)
            {
                AkkiNetworkIdentity identity = Player.GetComponent<AkkiNetworkIdentity>();
                if (identity != null)
                {
                    if (!AllAkkiNetworkIdentities.Contains(identity))
                    {
                        AllAkkiNetworkIdentities.Add(identity);
                    }
                }
            }
            for (int i = 0; i < RegisterNetworkObjects.Count; i++)
            {
                AkkiNetworkIdentity identity = RegisterNetworkObjects[i].GetComponent<AkkiNetworkIdentity>();
                if (identity != null)
                {
                    if (!AllAkkiNetworkIdentities.Contains(identity))
                    {
                        AllAkkiNetworkIdentities.Add(identity);
                    }
                }
            }

            SetUpRegisteredObjectNetworkIdentity();
        }

        #endregion

        #region PlayerSpawn

        private void RegisterPlayers()
        {
            string myid = AkkiGpgsManager.Instance.GetSelfParticipantId();
            List<Participant> allParticipants = AkkiGpgsManager.Instance.GetParticipants();

            for (int i = 0; i < allParticipants.Count; i++)
            {
                string currentPlayerId = allParticipants[i].ParticipantId;

                if (!NetworkIdIdDictionary.ContainsKey(currentPlayerId))
                {
                    GameObject playerObject = Instantiate(Player, Vector3.zero, Quaternion.identity);
                    AkkiNetworkIdentity PlayerId = playerObject.GetComponent<AkkiNetworkIdentity>();

                    if (PlayerId == null)
                    {
                        Debug.LogError("No AkkiNetworkIdentity Found in Player Root.");
                        Destroy(playerObject);
                        return;
                    }

                    if (currentPlayerId == myid)
                    {
                        PlayerId.NetworkId = myid;
                        PlayerId.IsLocalPlayer = true;
                    }
                    else
                    {
                        PlayerId.NetworkId = currentPlayerId;
                        PlayerId.IsLocalPlayer = false;
                    }

                    NetworkIdIdDictionary.Add(currentPlayerId, PlayerId);
                    NetworkIdObjectDictionary.Add(PlayerId, playerObject);
                }
            }
        }

        private void DeregisterPlayer(string id)
        {
            Debug.Log("Deregister Player");

            if (NetworkIdIdDictionary.ContainsKey(id))
            {
                AkkiNetworkIdentity playerId = NetworkIdIdDictionary[id];

                if (NetworkIdObjectDictionary.ContainsKey(playerId))
                {
                    GameObject playerGameObject = NetworkIdObjectDictionary[playerId] as GameObject;
                    Destroy(playerGameObject);
                    NetworkIdObjectDictionary.Remove(playerId);
                    NetworkIdIdDictionary.Remove(id);
                }
            }
        }

        private void DeregisterAllPlayer()
        {
            List<string> keys = new List<string>(NetworkIdIdDictionary.Keys);

            for (int i = 0; i < keys.Count; i++)
            {
                AkkiNetworkIdentity id = NetworkIdIdDictionary[keys[i]];
                if (id != null)
                {
                    if (NetworkIdObjectDictionary.ContainsKey(id))
                    {
                        GameObject obj = NetworkIdObjectDictionary[id];
                        Destroy(obj);
                        NetworkIdObjectDictionary.Remove(id);
                        NetworkIdIdDictionary.Remove(keys[i]);
                    }
                }
            }
        }

        #endregion /PlayerSpawn

        #region SpawnManager

        /// <summary>
        /// Spawn Other registered objects by its name.
        /// </summary>
        /// <param name="name">object name</param>
        public void SpawnPlayerByName(string name)
        {
            for (int i = 0; i < RegisterNetworkObjects.Count; i++)
            {
                if (RegisterNetworkObjects[i].name == name)
                {
                    GameObject otherObject = Instantiate<GameObject>(RegisterNetworkObjects[i].gameObject, Vector3.zero, Quaternion.identity);
                    AkkiNetworkIdentity PlayerId = otherObject.GetComponent<AkkiNetworkIdentity>();

                    if (PlayerId == null)
                    {
                        Debug.LogError("No AkkiNetworkIdentity Found in object Root.");
                        Destroy(otherObject);
                        break;
                    }

                    NetworkIdIdDictionary.Add(PlayerId.NetworkId, PlayerId);
                    NetworkIdObjectDictionary.Add(PlayerId, otherObject);
                    break;
                }
            }
        }

        /// <summary>
        /// Despawm object by its networkId.
        /// </summary>
        /// <param name="objectId"></param>
        public void DespawnPlayerByName(string objectId)
        {
            AkkiNetworkIdentity playerId = null;

            if (NetworkIdIdDictionary.ContainsKey(objectId))
            {
                playerId = NetworkIdIdDictionary[objectId];
            }

            if (playerId == null)
            {
                Debug.LogError("Player Network Id Not found.");
                return;
            }

            GameObject playerGameObject = null;

            if (NetworkIdObjectDictionary.ContainsKey(playerId))
            {
                playerGameObject = NetworkIdObjectDictionary[playerId] as GameObject;
                Destroy(playerGameObject);
                NetworkIdObjectDictionary.Remove(playerId);
            }

            NetworkIdIdDictionary.Remove(objectId);
        }

        #endregion

        #region SetupIdentity

        void SetUpRegisteredObjectNetworkIdentity()
        {
            for (int i = 0; i < RegisterNetworkObjects.Count; i++)
            {
                AkkiNetworkIdentity identity = RegisterNetworkObjects[i].GetComponent<AkkiNetworkIdentity>();
                identity.NetworkId = GetId() + i.ToString();
            }
        }

        string GetId()
        {
            return String.Format("{0},AkkiNetworkObject");
        }

        #endregion

        #region RealTimeRoomListener

        public void OnRoomSetupProgress(float percent)
        {
            if (RoomSetUp != null)
            {
                RoomSetUp.Invoke(percent);
            }
            Debug.Log("Network Manager OnRoom Setup : " + percent);
        }

        public void OnRoomConnected(bool success)
        {
            if (success)
            {
                RegisterPlayers();
            }
            else
            {
                Debug.Log("Room Not Connected..." + success);
            }

            if (RoomConnected != null)
            {
                RoomConnected.Invoke(success);
            }
        }

        public void OnLeftRoom()
        {
            Debug.Log("Player Left Room");
            DeregisterAllPlayer();

            for (int i = 0; i < AllAkkiNetworkIdentities.Count; i++)
            {
                AllAkkiNetworkIdentities[i].OnPlayerLeftRoom();
            }
        }

        public void OnParticipantLeft(Participant participant)
        {
            Debug.Log("Participant Left");
            DeregisterPlayer(participant.ParticipantId);

            for (int i = 0; i < AllAkkiNetworkIdentities.Count; i++)
            {
                AllAkkiNetworkIdentities[i].OnOtherPlayerLeftRoom(participant);
            }
        }

        public void OnPeersConnected(string[] participantIds)
        {
            foreach (var p in participantIds)
            {
                RegisterPlayers();
                for (int i = 0; i < AllAkkiNetworkIdentities.Count; i++)
                {
                    AllAkkiNetworkIdentities[i].OnConnectedRoom(p);
                }
            }
        }

        public void OnPeersDisconnected(string[] participantIds)
        {
            foreach (var p in participantIds)
            {
                Debug.Log("Peers Disconnected...");
                DeregisterPlayer(p);
                for (int i = 0; i < AllAkkiNetworkIdentities.Count; i++)
                {
                    AllAkkiNetworkIdentities[i].OnDisconnect(p);
                }
            }
        }

        public void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data)
        {
            Debug.Log("Message Received in network Manager...");
            if (AllAkkiNetworkIdentities.Count == 0)
            {
                Awake();
            }

            if (NetworkIdIdDictionary.ContainsKey(senderId))
            {
                Debug.Log("Key Contains....in Dictinary...");
                AkkiNetworkIdentity identity = NetworkIdIdDictionary[senderId];
                identity.OnMessageReceived(senderId, data);
            }
        }

        #endregion /RealTimeRoomListener

    }
}
