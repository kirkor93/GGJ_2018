using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vikipon : MonoBehaviour
{
	public PlayerScript.WikiPonType type;
	public bool isSmall = false;
	Animator anim;

	public void Init(bool small)
	{
		isSmall = small;
		anim = GetComponent<Animator>();

		if (isSmall)
			transform.localScale = Vector3.one * 0.25f;
		else
			transform.localScale = Vector3.one * 0.4f;

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

		transform.DOMove(targetNode.transform.position, time).OnComplete(() => anim.SetBool("moving", false));
	}

}
