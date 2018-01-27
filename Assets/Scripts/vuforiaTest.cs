using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class vuforiaTest : MonoBehaviour
{
	public List<modelController> corners;

	public bool scanned;
	public bool generatedBoard;
	public GameObject plane;

	void Update ()
	{
		if(!scanned)
		{
			int counter = 0;
			foreach(modelController obj in corners)
			{
				if(obj.wasScanned)
				{
					counter++;
				}
			}

			if(counter == corners.Count)
			{
				scanned = true;
			}
		}
		else if(!generatedBoard)
		{
			//CreatePlane();
			generatedBoard = true;
		}
	}

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
