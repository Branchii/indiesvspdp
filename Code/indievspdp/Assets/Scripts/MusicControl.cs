using UnityEngine;
using System.Collections;

public class MusicControl : MonoBehaviour
{
	AudioSource aus;
	// Use this for initialization
	void Awake()
	{
		Global.musCont = GetComponent<MusicControl>();
		aus = GetComponent<AudioSource>();
	}
	
	void Start ()
	{
	
	}

	public void ToggleMusic()
	{
		if (aus)
		{
			if (aus.isPlaying)
			{
				aus.Stop();
			}
			else
			{
				aus.Play();
				aus.timeSamples = 0;
			}
		}
	}
	
	// Update is called once per frame
	/*void Update ()
	{
		//Debug.Log(aus.timeSamples);
		if (aus.timeSamples >= (800568 - 512) || !aus.isPlaying)
		{
			if (!aus.isPlaying)
				aus.Play();
			aus.timeSamples = 177945 - 512;
			Debug.Log("huonoloop");
		}
	}*/
}
