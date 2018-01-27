﻿using System.Collections.Generic;
using Akki;
using Akki.Networking;
using GooglePlayGames.BasicApi.Multiplayer;
using UnityEngine;

public class Demo : MonoBehaviour
{
    public static Demo Instance;
    public GameObject canvas;
    public GameObject Backbutton;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        AkkiGpgsManager.Instance.Configure(true,
            AkkiGpgsManager.Instance.RealTimeRoomManager.InvitationManager.OnInvitationReceived,
            null, false, false, false, false);
    }

    void Start()
    {
        Movement m = AkkiNetworkManager.Instance.Player.GetComponent<Movement>();
        AkkiNetworkManager.Instance.RoomConnected += m.OnRoomJoined;
    }

    public void OnSignIn()
    {
        AkkiGpgsManager.Instance.SignIn();

        AkkiGpgsManager.Instance.RealTimeRoomManager.Listener = AkkiNetworkManager.Instance;
    }

    public void SignOut()
    {
        AkkiGpgsManager.Instance.SignOut();
    }

    public void ShowAchievementUi()
    {
        AkkiGpgsManager.Instance.ShowAchievementUi();
    }

    public void ShowLeaderboardUi()
    {
        AkkiGpgsManager.Instance.ShowLeaderboardUi();
    }

    public void SaveDataToCloud()
    {
        AkkiGpgsManager.Instance.SaveDataToCloud(null, "Hi");
    }

    public void LoadDataFromCloud()
    {
        AkkiGpgsManager.Instance.LoadDataFromCloud(null, LoadData);
    }

    public void LoadData(string data)
    {
        Debug.Log("Data Loaded");
    }

    public void ShowInbox()
    {
        AkkiGpgsManager.Instance.RealTimeRoomManager.AcceptFromInbox();
    }

    public void ShowVideoOverlay()
    {
        AkkiGpgsManager.Instance.RegisterVideoStateChangeListener();
        AkkiGpgsManager.Instance.ShowVideoCaptureOverlay();

    }

    public void CreateQuickMatch()
    {
        AkkiGpgsManager.Instance.CreateQuickGame();
    }

    public void CreateWithInvitationScreen()
    {
        AkkiGpgsManager.Instance.CreateWithInvitationScreen();
    }

    public void ShowWaitngRoomUi()
    {
        AkkiGpgsManager.Instance.ShowWaitingRoomUi();
    }

    public void LeaveRoom()
    {
        AkkiGpgsManager.Instance.LeaveRoom();
    }

    #region Utility

    private List<byte> _updateMessage = new List<byte>();

    public void SendMyMessage(float x, float z)
    {
        _updateMessage.Clear();
        _updateMessage.Add((byte)'U');
        _updateMessage.AddRange(System.BitConverter.GetBytes(x));
        _updateMessage.AddRange(System.BitConverter.GetBytes(z));
        byte[] messageToSend = _updateMessage.ToArray();
        AkkiGpgsManager.Instance.RealTimeRoomManager.SendMessageToAll(false, messageToSend);
    }

    #endregion
}
