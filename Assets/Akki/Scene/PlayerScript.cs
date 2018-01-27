using System.Collections.Generic;
using Akki;
using Akki.Networking;
using UnityEngine;

public class PlayerScript : AkkiNetworkBehaviour
{
    public enum SwipeDirection
    {
        Up,
        Down,
        Right,
        Left
    }

    private bool swiping = false;
    private bool eventSent = false;
    private Vector2 lastPosition;
    public float speed = 1;
    private float _nextBroadcastTime = 0;
    private Vector3 LastPlayerPosition;

    void Update()
    {
        if (!IsLocalPlayer)
        {
            return;
        }

        if (Input.touchCount == 0)
            return;

        if (Input.GetTouch(0).deltaPosition.sqrMagnitude != 0)
        {
            if (swiping == false)
            {
                swiping = true;
                lastPosition = Input.GetTouch(0).position;
                return;
            }
            if (!eventSent)
            {
                Vector2 direction = Input.GetTouch(0).position - lastPosition;

                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                {
                    if (direction.x > 0)
                        transform.Translate(Vector3.right * speed);
                    else
                        transform.Translate(Vector3.left * speed);
                }
                else
                {
                    if (direction.y > 0)
                        transform.Translate(Vector3.forward * speed);
                    else
                        transform.Translate(Vector3.back * speed);
                }

                eventSent = true;
            }
        }
        else
        {
            swiping = false;
            eventSent = false;
        }

        //Send message if position not matched..
        if (transform.position != LastPlayerPosition)
        {
            this.OnMessageSend();
            LastPlayerPosition = transform.position;
        }
    }

    private Vector3 _startPos;
    private Vector3 _destinationPos;
    private float _lastUpdateTime;
    private float _timePerUpdate = 0.16f;

    void FixedUpdate()
    {
        if (!IsLocalPlayer)
        {
            float pctDone = (Time.time - _lastUpdateTime) / _timePerUpdate;

            transform.position = Vector3.Lerp(_startPos, _destinationPos, pctDone);
        }
    }

    public void SetPlayerPosition(float x, float z)
    {
        _startPos = transform.position;
        _destinationPos = new Vector3(x, 0f, z);

        _lastUpdateTime = Time.time;
    }

    public override void OnMessageReceived(string senderId, byte[] data)
    {
        Debug.Log("On Message Received ... " + senderId + "   " + NetworkId + "    With LocalPlayer : " + IsLocalPlayer);
        char messageType = (char)data[0];
        if (messageType == 'U')
        {
            float posX = System.BitConverter.ToSingle(data, 1);
            float posZ = System.BitConverter.ToSingle(data, 5);

            SetPlayerPosition(posX, posZ);
        }
    }

    public void OnRoomJoined(bool success)
    {
        if (success)
        {
            Demo.Instance.canvas.SetActive(false);
            Demo.Instance.Backbutton.SetActive(true);
        }
    }

    private List<byte> _updateMessage = new List<byte>();

    public override void OnMessageSend()
    {
        _updateMessage.Clear();
        _updateMessage.Add((byte)'U');
        _updateMessage.AddRange(System.BitConverter.GetBytes(transform.position.x));
        _updateMessage.AddRange(System.BitConverter.GetBytes(transform.position.z));
        byte[] messageToSend = _updateMessage.ToArray();
        AkkiGpgsManager.Instance.SendMessageToAll(false, messageToSend);
    }
}