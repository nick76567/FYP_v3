using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicChangeManager : MonoBehaviour {

	// Use this for initialization

	public Slider Volume;
	public AudioSource myMusic;

	public static MusicChangeManager instance;

	void Awake()
	{
		if (instance == null)
			instance = this;
		else 
			{
			Destroy (gameObject);
			return;
		}

		DontDestroyOnLoad (gameObject);

	}
	// Update is called once per frame
	void Update () {
		myMusic.volume = Volume.value;
	}
}

// To pause the sound,
// MusicChangeManager.Instance.gameObject.GetComponent<AudioSource>().Pause()