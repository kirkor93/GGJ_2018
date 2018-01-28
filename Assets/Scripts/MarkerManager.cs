using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerManager : MonoBehaviour
{
	public AudioManager audioManager;
	public GameManager game;
	public List<Marker> markers;

	bool soundPlayed = false;

	private void Update()
	{
		List<Marker> visibleMarkers = new List<Marker>();

		foreach (Marker m in markers)
		{
			if (m.isShowed)
			{
				visibleMarkers.Add(m);
			}
		}

		List<Marker.Type> visibleTypes = new List<Marker.Type>();

		if (visibleMarkers.Count == 3)
		{
			foreach (Marker m in visibleMarkers)
			{
				if (!visibleTypes.Contains(m.myType))
				{
					visibleTypes.Add(m.myType);
				}
			}

			PlayerScript.WikiPonType wikiType = PlayerScript.WikiPonType.None;
			PlayerScript.MoveDirection dir = PlayerScript.MoveDirection.Left;
			int power = 0;

			foreach (Marker m in visibleMarkers)
			{
				if (m.myType == Marker.Type.vikipod)
				{
					wikiType = m.wikiType;



				}
				else if (m.myType == Marker.Type.direction)
				{
					dir = m.dir;
				}
				else if (m.myType == Marker.Type.incrementation)
				{
					power = m.power;
				}
			}

			if (visibleTypes.Count == 3)
			{
				if(!soundPlayed && !audioManager.GetComponent<AudioSource>().isPlaying)
				{
					if (wikiType == PlayerScript.WikiPonType.Blue)
					{
						audioManager.PlaySound("blue");
					}
					else if (wikiType == PlayerScript.WikiPonType.Red)
					{
						audioManager.PlaySound("red");
					}
					else if (wikiType == PlayerScript.WikiPonType.Yellow)
					{
						audioManager.PlaySound("yellow");
					}
					else if (wikiType == PlayerScript.WikiPonType.Purple)
					{
						audioManager.PlaySound("purple");
					}

					game.GetActivePlayer().SetActionStructureValues(wikiType, dir, power);
					soundPlayed = true;
				}
			}
			else
			{
				soundPlayed = false;
			}
		}
	}
}
