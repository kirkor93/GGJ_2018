using GooglePlayGames.BasicApi.Multiplayer;

namespace Akki.Networking
{

    public interface IRoomListener
    {
        void OnPlayerLeftRoom();
        void OnConnectedRoom(string id);
        void OnDisconnect(string id);
        void OnOtherPlayerLeftRoom(Participant otherPlayer);
        void OnMessageReceived(string senderId,byte[] data);
        void OnMessageSend();
    }
}
