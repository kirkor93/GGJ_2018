﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
	public Node[,] nodes = new Node[7, 9];
	public Transform horizontalLines;
	public GameObject chestPrefab;
	public List<GameObject> currentChests;

	public void Start()
	{
		for(int i=0; i < 9; i++)
		{
			for(int j = 0; j < 7; j++)
			{
				nodes[j, i] = horizontalLines.GetChild(i).GetChild(j).GetComponent<Node>();
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
		}
	}
}