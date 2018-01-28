using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour {

    public GameObject[] canvasesObjects; // 0 - menu, 1 - pauza, 2 - player 1, 3 - player 4
    public Board mainBoard;
    public int allCards;
    public int playerActiveId;
    public List<PlayerScript> playersList;
    public Transform[] obelisks;
    public GameObject[] wikiPonPrefabs; // 0 - Blue , 1- Purple, 2 - Red, 3 - Yello
    public List<GameObject> wikiPons1, wikiPons2;
    public float offSetNode = .14f;
    public bool isGameStarted = false;
    public float timeForPlayerMove;
    public int[] scoresValues;
    public Button actionButton;

    //UI
    public Text[] playerScoresTxt;
    public Image[] avatars;


	// Use this for initialization
	void Start () {
        InitData();
	}
	
	// Update is called once per frame
	void Update () {
		if (isGameStarted)
        {
            if (timeForPlayerMove >= 0)
            {
                timeForPlayerMove -= Time.deltaTime;
            }
        }
	}

    public void ClearEverything()
    {
        InitData();
    }

    public PlayerScript GetActivePlayer()
    {
        return playersList[playerActiveId];
    }

    public void SetStartGame(bool startFlag)
    {
        isGameStarted = startFlag;
    }

    public void SetPlayerMovementTimer(float newTime = 1f)
    {
        timeForPlayerMove = newTime;
    }

    internal void InitData()
    {
        allCards = 72;
        timeForPlayerMove = -10f;

        for (int i = 0; i < obelisks.Length; i++)
        {
            obelisks[i].DOKill();
            obelisks[i].transform.position = mainBoard.startPlayerNodes[i].position;
        }

        if (wikiPons1.Count > 0)
        {
            foreach (GameObject w in wikiPons1)
                GameObject.Destroy(w);
            wikiPons1.Clear();
        }
        if (wikiPons2.Count > 0)
        {
            foreach (GameObject w in wikiPons1)
                GameObject.Destroy(w);
            wikiPons2.Clear();
        }

        SetCanvas(0, false);
        SetCanvas(1, false);
        SetCanvas(2, true);

        for (int i = 0; i < scoresValues.Length; i++)
            scoresValues[i] = 0;
        SetScoresUI();

        playerActiveId = 0;
        SetStartGame(true);
        actionButton.interactable = false;
        avatars[0].color = new Color(1, 1, 1, 1);
        avatars[1].color = new Color(1, 1, 1,.3f);
    }

    internal void EndThisGame()
    {

    }

    public bool CanPlay()
    {
        return isGameStarted && timeForPlayerMove < 0;
    }

    public void ChangeActivePlayer()
    {
        SetPlayerMovementTimer();
        avatars[playerActiveId].DOKill();
        avatars[playerActiveId].color = new Color(1,1,1,0.3f);
        ++playerActiveId;

        if (playerActiveId > 1)
            playerActiveId = 0;
        avatars[playerActiveId].DOKill();
        avatars[playerActiveId].color = new Color(1, 1, 1, 1f);

        actionButton.interactable = false;
        SetScoresUI();
    }

    public void AddNewPlayer(PlayerScript nP)
    {
        playersList.Add(nP);

        if (playersList.Count > 1)
        {
            SetStartGame(true);
        }
    }

    public void CreateWikiPon(PlayerScript.WikiPonType wikiPonT, int wikiPonCounter, PlayerScript playerS)
    {
        GameObject newWikiPon = null;

        if (wikiPonT == PlayerScript.WikiPonType.Red)
        {
            newWikiPon = wikiPonPrefabs[0];
        }
        else if (wikiPonT == PlayerScript.WikiPonType.Yellow)
        {
            newWikiPon = wikiPonPrefabs[1];
        }
        else if (wikiPonT == PlayerScript.WikiPonType.Purple)
        {
            newWikiPon = wikiPonPrefabs[2];
        }
        else if (wikiPonT == PlayerScript.WikiPonType.Blue)
        {
            newWikiPon = wikiPonPrefabs[3];
        }

        for (int i = 0; i < wikiPonCounter; i++)
        {
            GameObject newWiki = GameObject.Instantiate(newWikiPon);
            newWiki.transform.position = playerS.actualNode.transform.position;

            if (playerS.idPlayer == 0)
                wikiPons1.Add(newWiki);
            else
                wikiPons2.Add(newWiki);

            //TO DO - animations
        }
    }

    public void MakeComboAction()
    {
        GetActivePlayer().MakeAction();
    }

    public void SetPause(bool pauseFlag)
    {
        if (pauseFlag)
        {
            SetCanvas(0, true);
        }
        else
        {
            SetCanvas(0, false);
        }
    }

    public void SetCanvas(int id, bool isActiveCanvas)
    {
        canvasesObjects[id].SetActive(isActiveCanvas);
    }

    public void SetScoresUI()
    {
        playerScoresTxt[0].text = "X" + scoresValues[0].ToString();
        playerScoresTxt[1].text = "X" + scoresValues[1].ToString();
    }
}
