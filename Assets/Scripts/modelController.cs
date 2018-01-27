using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class modelController : MonoBehaviour
{
	public bool wasScanned = false;
	public Vector3 startPos;
	
	void Update ()
	{
		if(!wasScanned && GetComponentInChildren<MeshRenderer>().enabled)
		{
			//startPos = transform.GetChild(0).position;
			wasScanned = true;
		}
	}
}
