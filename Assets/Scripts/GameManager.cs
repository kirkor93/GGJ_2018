using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour {

    public Board mainBoard;
    public int allCards;
    public int playerActiveId;
    public List<PlayerScript> playersList;
    public Transform[] obelisks;
    public GameObject[] wikiPonPrefabs; // 0 - Blue , 1- Purple, 2 - Red, 3 - Yello
    public List<GameObject> wikiPons1, wikiPons2;
    public float offSetNode = .14f;

    //UI
    Text[] playerScoresTxt;


	// Use this for initialization
	void Start () {
        InitData();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public PlayerScript GetActivePlayer()
    {
        return playersList[playerActiveId];
    }

    internal void InitData()
    {
        allCards = 72;

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
    }

    internal void RemoveCard(int countCard)
    {
        allCards -= countCard;

        if (allCards <= 0)
        {
            EndThisGame();
        }
    }

    internal void EndThisGame()
    {

    }

    public void CreateWikiPon(PlayerScript.WikiPonType wikiPonT, int wikiPonCounter, PlayerScript playerS)
    {
        GameObject newWikiPon = null;

        if (wikiPonT == PlayerScript.WikiPonType.Blue)
        {
            newWikiPon = wikiPonPrefabs[0];
        }
        else if (wikiPonT == PlayerScript.WikiPonType.Purple)
        {
            newWikiPon = wikiPonPrefabs[1];
        }
        else if (wikiPonT == PlayerScript.WikiPonType.Red)
        {
            newWikiPon = wikiPonPrefabs[2];
        }
        else if (wikiPonT == PlayerScript.WikiPonType.Yellow)
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
}
