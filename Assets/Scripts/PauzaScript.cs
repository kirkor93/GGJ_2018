using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauzaScript : MonoBehaviour {

    public Text[] scoresTxt;
    public GameManager gM;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetEnd()
    {
        gM.SetCanvas(1, true);
    }

    public void SetPauzeScores()
    {
        if (gM.scoresValues[0] > gM.scoresValues[1])
        {
            scoresTxt[0].text = "X" + gM.scoresValues[0].ToString();
            scoresTxt[1].text = "X" + gM.scoresValues[1].ToString();
        }
        else
        {
            scoresTxt[0].text = "X" + gM.scoresValues[1].ToString();
            scoresTxt[1].text = "X" + gM.scoresValues[0].ToString();
        }
    }
}
