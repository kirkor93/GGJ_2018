using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vikipon : MonoBehaviour
{
	public PlayerScript.WikiPonType type;
	public bool isSmall = false;
	Animator anim;
	Board board;

	private void Update()
	{
		transform.localEulerAngles = Vector3.zero;
	}

	public void Init(bool small, Board _board)
	{
		board = _board;
		isSmall = small;
		anim = GetComponentInChildren<Animator>();

		if (isSmall)
			transform.localScale = Vector3.one * 0.125f;
		else
			transform.localScale = Vector3.one * 0.2f;

		if(type == PlayerScript.WikiPonType.Blue)
		{
			anim.SetBool("isSmall", isSmall);

			if (isSmall)
				anim.Play("small", 0, 0);
			else
				anim.Play("big", 0, 0);
		}
	}
	
	public void Move(Node currentNode, Node targetNode)
	{
		anim.SetBool("moving", true);
		float time = 1.0f;

		if(currentNode.idX == targetNode.idX)
		{
			time = 1.0f * Mathf.Abs(currentNode.idY - targetNode.idY);
		}
		else
		{
			time = 1.0f * Mathf.Abs(currentNode.idX - targetNode.idX);
		}

		transform.parent = targetNode.transform;

		transform.DOLocalMove(Vector3.zero, time).OnComplete(() =>
		{
			anim.SetBool("moving", false);
			board.SpawnParticles(currentNode, targetNode, type);

			if(targetNode.haveChest)
			{
				//DAĆ PUNKTA
				targetNode.haveChest = false;
				targetNode.chest.SetActive(false);
				board.currentChests.Remove(targetNode.chest);
			}
		});
	}

}
