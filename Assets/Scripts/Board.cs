using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Transform[] startPlayerNodes; // just for two players
    public int nodesCoutX, nodesCountY;
	public Node[,] nodes = new Node[7, 10];
	public Transform horizontalLines;
	public GameObject chestPrefab;
	public GameObject particlesPrefab;
	public List<GameObject> currentChests;

	public void Start()
	{
		for(int i=0; i < 10; i++)
		{
			for(int j = 0; j < 7; j++)
			{
				nodes[j, i] = horizontalLines.GetChild(i).GetChild(j).GetComponent<Node>();
                nodes[j, i].idX = i;
                nodes[j, i].idY = j;
                nodes[j, i].colorType = PlayerScript.WikiPonType.None;
			}
		}

		PlaceChests(3);
	}

	void PlaceChests(int number)
	{
		for(int i=0; i< number; i++)
		{
			int rand = 0;

			if(i == 0)
			{
				rand = Random.Range(1, 3);
			}
			else if(i == 1)
			{
				rand = Random.Range(3, 6);
			}
			else
			{
				rand = Random.Range(6, 8);
			}

			Node targetNode = nodes[Random.Range(0, 7), rand];

			while(targetNode.haveChest)
			{
				targetNode = nodes[Random.Range(0, 7), rand];
			}

			targetNode.haveChest = true;
			GameObject chest = Instantiate(chestPrefab);
			chest.transform.parent = targetNode.transform;
			chest.transform.localPosition = Vector3.zero;
			currentChests.Add(chest);
			targetNode.chest = chest;
		}
	}

	public void SpawnParticles(Node startingNode,  Node targetNode, PlayerScript.WikiPonType type)
	{
		GameObject newParticles = Instantiate(particlesPrefab);
		newParticles.transform.parent = startingNode.transform;

		var main = newParticles.GetComponentInChildren<ParticleSystem>().main;

		if (type == PlayerScript.WikiPonType.Red)
		{
			main.startColor = Color.red;
		}
		else if (type == PlayerScript.WikiPonType.Yellow)
		{
			main.startColor = Color.yellow;
		}
		else if (type == PlayerScript.WikiPonType.Blue)
		{
			main.startColor = Color.blue;
		}
		else if (type == PlayerScript.WikiPonType.Purple)
		{
			main.startColor = Color.magenta;
		}

		if (startingNode.idX == targetNode.idX)
		{
			if(startingNode.idY > targetNode.idY)
			{
				newParticles.transform.localEulerAngles = new Vector3(0, 90, 0);
			}
			else
			{
				newParticles.transform.localEulerAngles = new Vector3(0, 90, 0);
			}
		}
		else
		{
			if (startingNode.idX < targetNode.idX)
			{
				newParticles.transform.localEulerAngles = Vector3.zero;

			}
		}
	}
}
