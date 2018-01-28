using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScript : MonoBehaviour
{

    public Text[] scoresTxt;
    public GameManager gM;
    public Sprite[] bgWinners;
    public Image bgWinner;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetEndScores()
    {
        if (gM.scoresValues[0] > gM.scoresValues[1])
        {
            scoresTxt[0].text = "X" + gM.scoresValues[1].ToString();
            scoresTxt[1].text = "X" + gM.scoresValues[0].ToString();
            bgWinner.sprite = bgWinners[0];
        }
        else
        {
            scoresTxt[0].text = "X" + gM.scoresValues[0].ToString();
            scoresTxt[1].text = "X" + gM.scoresValues[1].ToString();
            bgWinner.sprite = bgWinners[1];
        }
    }
}
