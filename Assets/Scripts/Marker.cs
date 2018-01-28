using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{
	public PlayerScript.WikiPonType wikiType;
	public PlayerScript.MoveDirection dir;
	public int power;

	public Type myType;

	public enum Type
	{
		vikipod,
		direction,
		incrementation
	}

	public bool isShowed = false;

	private void Update()
	{
		if (!isShowed && transform.GetComponentInChildren<MeshRenderer>().enabled)
		{
			isShowed = true;
		}
		else if(isShowed && !transform.GetComponentInChildren<MeshRenderer>().enabled)
		{
			isShowed = false;
		}
	}
}
