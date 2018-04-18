using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DontDestroyAudios : MonoBehaviour {
	//public Slider Volume;
	//public AudioSource myMusic;
	public Text test;

	static DontDestroyAudios instance = null;
	void Awake()
	{
		if (instance != null)
		{
			if (instance != this)
				Destroy(instance.gameObject);
			else
				Destroy(this.gameObject);
			return;
		}
		else
		{
			instance = this;
		}

		DontDestroyOnLoad(this.gameObject);
	}

//	void update()
//	{
//		myMusic.volume = Volume.value;
//	}
		

}