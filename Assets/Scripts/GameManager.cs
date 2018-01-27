using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour {

    public Board mainBoard;
    public int allCards;
    public int playerActiveId;
    public List<PlayerScript> playersList;
    public Transform[] obelisks;
    public GameObject[] wikiPonPrefabs;


	// Use this for initialization
	void Start () {
        InitData();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    internal void InitData()
    {
        allCards = 72;

        for (int i = 0; i < obelisks.Length; i++)
        {
            obelisks[i].DOKill();
            obelisks[i].transform.position = mainBoard.startPlayerNodes[i].position;
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

    public void CreateWikiPon(PlayerScript.WikiPonType wikiPonT)
    {
        if (wikiPonT == PlayerScript.WikiPonType.Blue)
        {

        }
        else if (wikiPonT == PlayerScript.WikiPonType.Purple)
        {

        }
        else if (wikiPonT == PlayerScript.WikiPonType.Red)
        {

        }
        else if (wikiPonT == PlayerScript.WikiPonType.Yellow)
        {

        }
    }
}
