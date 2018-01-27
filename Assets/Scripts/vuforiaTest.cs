using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class vuforiaTest : MonoBehaviour
{
	//public Camera cam;
	//public List<modelController> corners;
	public modelController model;
	public GameObject targetImage;
	public bool scanned;
	public bool generatedBoard;
	public GameObject plane;
	//public Text camText;
	void Update ()
	{
		if(!scanned)
		{
			if (model.wasScanned)
				scanned = true;
			//int counter = 0;
			//foreach(modelController obj in corners)
			//{
			//	if(obj.wasScanned)
			//	{
			//		counter++;
			//	}
			//}

			//if(counter == corners.Count)
			//{
			//	scanned = true;
			//}
		}
		else if(!generatedBoard)
		{
			plane.transform.position = targetImage.transform.position - new Vector3(0, 0.1f, 0);
			plane.transform.eulerAngles = targetImage.transform.eulerAngles - new Vector3(90, 0, 0);
			plane.SetActive(true);
			generatedBoard = true;
		}
		else
		{
			if(model.GetComponent<MeshRenderer>().enabled)
			{
				if (!plane.activeSelf)
					plane.SetActive(true);

				plane.transform.position = targetImage.transform.position - new Vector3(0,0.1f,0);
				plane.transform.eulerAngles = targetImage.transform.eulerAngles - new Vector3(90, 0, 0);
			}
			else if(plane.activeSelf)
			{
				plane.SetActive(false);
			}
		}


		//camText.text = "Camera rotation = " + cam.transform.eulerAngles;
		//else
		//{
		//	FixPos();
		//}
	}

	//void FixPos()
	//{
	//	float minX = 100000;
	//	float maxX = -100000;
	//	float minZ = 100000;
	//	float maxZ = -100000;

	//	foreach (modelController m in corners)
	//	{
	//		if (m.startPos.x < minX)
	//		{
	//			minX = m.transform.position.x;
	//		}

	//		if (m.startPos.x > maxX)
	//		{
	//			maxX = m.transform.position.x;
	//		}

	//		if (m.startPos.z > maxZ)
	//		{
	//			maxZ = m.transform.position.z;
	//		}

	//		if (m.startPos.z < minZ)
	//		{
	//			minZ = m.transform.position.z;
	//		}
	//	}

	//	plane.transform.position = new Vector3((minX + maxX) / 2, 0, (minZ + maxZ) / 2);
	//	plane.SetActive(true);
	//}

	//void SpawnPlane()
	//{
	//	float minX = 100000;
	//	float maxX = -100000;
	//	float minZ = 100000;
	//	float maxZ = -100000;

	//	foreach(modelController m in corners)
	//	{
	//		if(m.startPos.x < minX)
	//		{
	//			minX = m.startPos.x;
	//		}

	//		if(m.startPos.x > maxX)
	//		{
	//			maxX = m.startPos.x;
	//		}

	//		if(m.startPos.z > maxZ)
	//		{
	//			maxZ = m.startPos.z;
	//		}

	//		if(m.startPos.z < minZ)
	//		{
	//			minZ = m.startPos.z;
	//		}
	//	}

	//	plane.transform.position = new Vector3((minX + maxX) / 2, 0, (minZ + maxZ) / 2);
	//	plane.SetActive(true);
	//}

	//void CreatePlane()
	//{
	//	plane = new GameObject("Plane");
	//	MeshFilter mf = (MeshFilter)plane.AddComponent(typeof(MeshFilter));
	//	mf.mesh = CreateMesh();
	//	MeshRenderer renderer = plane.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
	//	renderer.material.shader = Shader.Find("Particles/Additive");
	//	Texture2D tex = new Texture2D(1, 1);
	//	tex.SetPixel(0, 0, Color.blue);
	//	tex.Apply();
	//	renderer.material.mainTexture = tex;
	//	renderer.material.color = Color.blue;
	//}

	//Mesh CreateMesh()
	//{
	//	Mesh m = new Mesh();
	//	m.name = "ScriptedMesh";
	//	m.vertices = new Vector3[] 
	//	{
	//	 new Vector3(corners[0].startPos.x, 0.0f, corners[0].startPos.z),
	//	 new Vector3(corners[1].startPos.x, 0.0f, corners[1].startPos.z),
	//	 new Vector3(corners[2].startPos.x, 0.0f, corners[2].startPos.z),
	//	 new Vector3(corners[3].startPos.x, 0.0f, corners[3].startPos.z),
	//	 };
	//	m.uv = new Vector2[]
	//	{
	//	 new Vector2 (0, 0),
	//	 new Vector2 (0, 1),
	//	 new Vector2(1, 1),
	//	 new Vector2 (1, 0)
	//	 };
	//	m.triangles = new int[] { 0, 1, 2, 0, 2, 3 };
	//	m.RecalculateNormals();

	//	return m;
	//}
}
