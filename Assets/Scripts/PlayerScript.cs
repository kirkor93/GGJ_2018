using System.Collections.Generic;
using Akki;
using Akki.Networking;
using UnityEngine;

public class PlayerScript : AkkiNetworkBehaviour
{
    public GameManager gM;

    public enum SwipeDirection
    {
        Up,
        Down,
        Right,
        Left
    }

    /// <summary>
    /// //////////// 3 action parameteres
    /// </summary>
    public enum WikiPonType
    {
        Red,
        Yellow,
        Purple,
        Blue,
        None
    }

    public enum MoveDirection
    {
        Up,
        Right,
        Left
    }

    public int incrementation = 0;

    public Node actualNode;
    public Transform playerObelisk;
    public bool isActivePlayer;
    public int idPlayer = 0; // 0 - first, 1 - left
    public List<Node> newNodesWay;
    public int playerScore;

    public struct ActionStructure
    {
        public WikiPonType wikiPonType;
        public MoveDirection moveType;
        public int incrementatorType;

        public ActionStructure (WikiPonType w, MoveDirection m, int iC)
        {
            this.wikiPonType = w;
            this.moveType = m;
            this.incrementatorType = iC;
        }
};

    public ActionStructure actionStruct;
    public Transform startNode;
    private bool swiping = false;
    private bool eventSent = false;
    private Vector2 lastPosition;
    public float speed = 1;
    private float _nextBroadcastTime = 0;
    private Vector3 LastPlayerPosition;

    public void InitData()
    {

    }

    void Update()
    {
        if (!IsLocalPlayer)
        {
            return;
        }

        /*
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
        */
    }

    private Vector3 _startPos;
    private Vector3 _destinationPos;
    private float _lastUpdateTime;
    private float _timePerUpdate = 0.16f;

    void FixedUpdate()
    {
        /*
        if (!IsLocalPlayer)
        {
            float pctDone = (Time.time - _lastUpdateTime) / _timePerUpdate;

            transform.position = Vector3.Lerp(_startPos, _destinationPos, pctDone);
        }
        */
    }

    public void SetPlayerPosition(float x, float z)
    {
        _startPos = transform.position;
        _destinationPos = new Vector3(x, 0f, z);
        //_lastUpdateTime = Time.time;
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
            //Demo.Instance.canvas.SetActive(false);
            Demo.Instance.Backbutton.SetActive(true);
            gM.SetCanvas(0, false);
            gM.SetCanvas(2, true);
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

    /////////////////////// game's actions

    public void InitData(GameManager newGm, Transform newStartPos, Node firstNode, Transform newObelisk)
    {
        gM = newGm;
        startNode = newStartPos;
        actualNode = firstNode;
        playerObelisk = newObelisk;
        transform.position = startNode.position;

        ActionStructure actionStruct = new ActionStructure( WikiPonType.None, MoveDirection.Up, 0);
    }

    //Move
    public void MovePlayer()
    {
        newNodesWay.Clear();

        if (idPlayer == 0)
        {
            if (actionStruct.moveType == MoveDirection.Up)
            {
                int newID = actionStruct.incrementatorType;

                for (int i = actualNode.idX; i <= actualNode.idX + actionStruct.incrementatorType; i++)
                {
                    if (gM.mainBoard.nodes[i, actualNode.idY].colorType == actionStruct.wikiPonType)
                        ++newID;
                }

                actionStruct.incrementatorType = newID;

                for (int i = actualNode.idX; i <= actualNode.idX + actionStruct.incrementatorType; i++)
                {
                    if (i < gM.mainBoard.nodesCoutX && (gM.mainBoard.nodes[i, actualNode.idY].colorType == actionStruct.wikiPonType || gM.mainBoard.nodes[i, actualNode.idY].colorType == PlayerScript.WikiPonType.None))
                    {
                        actualNode = gM.mainBoard.nodes[i, actualNode.idY];
                        AddNodeToWay();
                    }
                    else
                        break;
                }
            }
            else if (actionStruct.moveType == MoveDirection.Right)
            {
                int newID = actionStruct.incrementatorType;

                for (int i = actualNode.idY; i <= actualNode.idY + actionStruct.incrementatorType; i++)
                {
                    if (gM.mainBoard.nodes[actualNode.idX, i].colorType == actionStruct.wikiPonType)
                        ++newID;
                }

                actionStruct.incrementatorType = newID;
            
                for (int i = actualNode.idY; i <= actualNode.idY + actionStruct.incrementatorType; i++)
                {
                    if (i < gM.mainBoard.nodesCountY && (gM.mainBoard.nodes[actualNode.idX, i].colorType == actionStruct.wikiPonType || gM.mainBoard.nodes[actualNode.idX, i].colorType == PlayerScript.WikiPonType.None))
                    {
                        actualNode = gM.mainBoard.nodes[actualNode.idX, i];
                        AddNodeToWay();
                    }
                    else
                        break;
                }
            }
            else
            {
                int newID = actionStruct.incrementatorType;

                for (int i = actualNode.idY; i >= actualNode.idY - actionStruct.incrementatorType; i--)
                {
                    if (gM.mainBoard.nodes[actualNode.idX, i].colorType == actionStruct.wikiPonType)
                        ++newID;
                }

                actionStruct.incrementatorType = newID;

                for (int i = actualNode.idY; i >= actualNode.idY - actionStruct.incrementatorType; i--)
                {
                    if (i > -1 && (gM.mainBoard.nodes[actualNode.idX, i].colorType == actionStruct.wikiPonType || gM.mainBoard.nodes[actualNode.idX, i].colorType == PlayerScript.WikiPonType.None))
                    {
                        actualNode = gM.mainBoard.nodes[actualNode.idX, i];
                        AddNodeToWay();
                    }
                    else
                        break;
                }
            }
        }
        else
        {
            if (actionStruct.moveType == MoveDirection.Up)
            {
                int newID = actionStruct.incrementatorType;

                for (int i = actualNode.idX; i <= actualNode.idX + actionStruct.incrementatorType; i++)
                {
                    if (gM.mainBoard.nodes[i, actualNode.idY].colorType == actionStruct.wikiPonType)
                        ++newID;
                }

                actionStruct.incrementatorType = newID;

                for (int i = actualNode.idX; i <= actualNode.idX + actionStruct.incrementatorType; i++)
                {
                    if (i < gM.mainBoard.nodesCoutX && (gM.mainBoard.nodes[i, actualNode.idY].colorType == actionStruct.wikiPonType || gM.mainBoard.nodes[i, actualNode.idY].colorType == PlayerScript.WikiPonType.None))
                    {
                        actualNode = gM.mainBoard.nodes[i, actualNode.idY];
                        AddNodeToWay();
                    }
                    else
                        break;
                }
            }
            else if (actionStruct.moveType == MoveDirection.Right)
            {
                int newID = actionStruct.incrementatorType;

                for (int i = actualNode.idY; i <= actualNode.idY + actionStruct.incrementatorType; i++)
                {
                    if (gM.mainBoard.nodes[actualNode.idX, i].colorType == actionStruct.wikiPonType)
                        ++newID;
                }

                actionStruct.incrementatorType = newID;

                for (int i = actualNode.idY; i <= actualNode.idY + actionStruct.incrementatorType; i++)
                {
                    if (i < gM.mainBoard.nodesCountY && (gM.mainBoard.nodes[actualNode.idX, i].colorType == actionStruct.wikiPonType || gM.mainBoard.nodes[actualNode.idX, i].colorType == PlayerScript.WikiPonType.None))
                    {
                        actualNode = gM.mainBoard.nodes[actualNode.idX, i];
                        AddNodeToWay();
                    }
                    else
                        break;
                }
            }
            else
            {
                int newID = actionStruct.incrementatorType;

                for (int i = actualNode.idY; i >= actualNode.idY - actionStruct.incrementatorType; i--)
                {
                    if (gM.mainBoard.nodes[actualNode.idX, i].colorType == actionStruct.wikiPonType)
                        ++newID;
                }

                actionStruct.incrementatorType = newID;

                for (int i = actualNode.idY; i >= actualNode.idY - actionStruct.incrementatorType; i--)
                {
                    if (i > -1 && (gM.mainBoard.nodes[actualNode.idX, i].colorType == actionStruct.wikiPonType || gM.mainBoard.nodes[actualNode.idX, i].colorType == PlayerScript.WikiPonType.None))
                    {
                        actualNode = gM.mainBoard.nodes[actualNode.idX, i];
                        AddNodeToWay();
                    }
                    else
                        break;
                }
            }
        }
    }

    private void AddNodeToWay()
    {
        actualNode.colorType = actionStruct.wikiPonType;
        newNodesWay.Add(actualNode);
    }

    public void VibratePhone()
    {
        Handheld.Vibrate();
    }

    public void SetActionStructureValues(PlayerScript.WikiPonType wP, PlayerScript.MoveDirection mD, int iC)
    {
        actionStruct.wikiPonType = wP;
        actionStruct.moveType = mD;
        actionStruct.incrementatorType = iC;

        gM.actionButton.interactable = true;
    }

    public void MakeAction()
    {
        gM.actionButton.interactable = false;
        gM.CreateWikiPon(actionStruct.wikiPonType, actionStruct.incrementatorType, this);
        MovePlayer();
    }
}