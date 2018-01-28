using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vikipon : MonoBehaviour
{
	public bool isSmall = false;
	Animator anim;

	public void Init(bool small)
	{
		isSmall = small;
		anim = GetComponent<Animator>();
		anim.SetBool("isSmall", isSmall);
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

		transform.DOMove(targetNode.transform.position, time).OnComplete(() => anim.SetBool("moving", false));
	}

}
