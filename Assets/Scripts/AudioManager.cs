using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public AudioClip clickClip;
	public AudioClip endRoundClip;
	public AudioClip startRoundClip;
	public AudioClip pointsForEndClip;
	public AudioClip pointsForObeliskClip;
	public AudioClip redClip;
	public AudioClip yellowClip;
	public AudioClip blueClip;
	public AudioClip purpleClip;

	AudioSource source;

	void Start ()
	{
		source = GetComponent<AudioSource>();
	}
	
	public void PlaySound(string type)
	{
		if(type == "click")
		{
			source.clip = clickClip;
		}
		else if(type == "endRound")
		{
			source.clip = endRoundClip;
		}
		else if(type == "startRound")
		{
			source.clip = startRoundClip;
		}
		else if(type == "pointsEnd")
		{
			source.clip = pointsForEndClip;
		}
		else if(type == "pointsObelisk")
		{
			source.clip = pointsForObeliskClip;
		}
		else if(type == "red")
		{
			source.clip = redClip;
		}
		else if (type == "blue")
		{
			source.clip = blueClip;
		}
		else if (type == "yellow")
		{
			source.clip = yellowClip;
		}
		else if (type == "purple")
		{
			source.clip = purpleClip;
		}

		source.Play();
	}
}
