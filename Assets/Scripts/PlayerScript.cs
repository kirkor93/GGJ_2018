using System.Collections.Generic;
//using Akki;
//using Akki.Networking;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameManager gM;

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


    void Update()
    {
    }

    private Vector3 _startPos;
    private Vector3 _destinationPos;
    private float _lastUpdateTime;
    private float _timePerUpdate = 0.16f;

    public void SetPlayerPosition(float x, float z)
    {
        _startPos = transform.position;
        _destinationPos = new Vector3(x, 0f, z);
        //_lastUpdateTime = Time.time;
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
        //SetActionStructureValues(WikiPonType.Red, MoveDirection.Right, 3);
    }

    //Move
    public void MovePlayer()
    {
        if (idPlayer == 0)
        {
            if (actionStruct.moveType == MoveDirection.Right)
            {
                /*
                int newID = actionStruct.incrementatorType;

                for (int i = actualNode.idX; i <= actualNode.idX + actionStruct.incrementatorType; i++)
                {
                    if (gM.mainBoard.nodes[i, actualNode.idY].colorType == actionStruct.wikiPonType)
                        ++newID;
                }

                actionStruct.incrementatorType = newID;
                */
                for (int i = actualNode.idX; i < actualNode.idX + actionStruct.incrementatorType; i++)
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
            else if (actionStruct.moveType == MoveDirection.Up)
            {
                /*
                int newID = actionStruct.incrementatorType;

                for (int i = actualNode.idY; i <= actualNode.idY + actionStruct.incrementatorType; i++)
                {
                    if (i < gM.mainBoard.nodesCountY && gM.mainBoard.nodes[actualNode.idX, i].colorType == actionStruct.wikiPonType)
                        ++newID;
                }

                actionStruct.incrementatorType = newID;
                */

                for (int i = actualNode.idY; i < actualNode.idY + actionStruct.incrementatorType; i++)
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
                /*
                int newID = actionStruct.incrementatorType;

                for (int i = actualNode.idX; i >= actualNode.idX - actionStruct.incrementatorType; i--)
                {
                    if (i >= 0 && gM.mainBoard.nodes[i, actualNode.idY].colorType == actionStruct.wikiPonType)
                        ++newID;
                }

                actionStruct.incrementatorType = newID;
                */
                for (int i = actualNode.idX; i > actualNode.idX - actionStruct.incrementatorType; i--)
                {
                    if (i >= 0 && (gM.mainBoard.nodes[i, actualNode.idY].colorType == actionStruct.wikiPonType || gM.mainBoard.nodes[i, actualNode.idY].colorType == PlayerScript.WikiPonType.None))
                    {
                        actualNode = gM.mainBoard.nodes[i, actualNode.idY];
                        AddNodeToWay();
                    }
                    else
                        break;
                }
            }
        }
        else
        {
            Debug.Log(idPlayer + "    " + actionStruct.moveType);
            if (actionStruct.moveType == MoveDirection.Left)
            {
                /*
                int newID = actionStruct.incrementatorType;

                for (int i = actualNode.idX; i <= actualNode.idX + actionStruct.incrementatorType; i++)
                {
                    if (i < gM.mainBoard.nodesCoutX && gM.mainBoard.nodes[i, actualNode.idY].colorType == actionStruct.wikiPonType)
                        ++newID;
                }

                actionStruct.incrementatorType = newID;
                */
                for (int i = actualNode.idX; i < actualNode.idX + actionStruct.incrementatorType; i++)
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
                /*
                int newID = actionStruct.incrementatorType;

                for (int i = actualNode.idY; i >= actualNode.idY - actionStruct.incrementatorType; i--)
                {
                    if (i > 0 && gM.mainBoard.nodes[i, actualNode.idX].colorType == actionStruct.wikiPonType)
                        ++newID;
                }

                actionStruct.incrementatorType = newID;
                */

                for (int i = actualNode.idX; i > actualNode.idX - actionStruct.incrementatorType; i--)
                {
                    if (i > -1 && (gM.mainBoard.nodes[i, actualNode.idY].colorType == actionStruct.wikiPonType || gM.mainBoard.nodes[i, actualNode.idY].colorType == PlayerScript.WikiPonType.None))
                    {
                        actualNode = gM.mainBoard.nodes[i, actualNode.idY];
                        AddNodeToWay();
                    }
                    else
                        break;
                }
            }
            else
            {
                /*
                int newID = actionStruct.incrementatorType;

                for (int i = actualNode.idY; i >= actualNode.idY - actionStruct.incrementatorType; i--)
                {
                    if (i > -1 && gM.mainBoard.nodes[actualNode.idX, i].colorType == actionStruct.wikiPonType)
                        ++newID;
                }

                actionStruct.incrementatorType = newID;
                */
                for (int i = actualNode.idY; i > actualNode.idY - actionStruct.incrementatorType; i--)
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
        Debug.Log(idPlayer.ToString() + "      "+ actualNode.name);
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
        newNodesWay.Clear();
        newNodesWay = new List<Node>();

        gM.actionButton.interactable = false;
        Node lastN = actualNode;
        MovePlayer();

        gM.CreateWikiPon(actionStruct.wikiPonType, actionStruct.incrementatorType, this, lastN);
        gM.ChangeActivePlayer();
        //SetActionStructureValues(WikiPonType.Red, MoveDirection.Up, 3);
    }
}