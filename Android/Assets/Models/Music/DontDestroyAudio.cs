using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DontDestroyAudio : MonoBehaviour {

	public Slider Volume;
	public AudioSource myMusic;
    static DontDestroyAudio instance = null;

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

	void Update () {
		myMusic.volume = Volume.value;
	}

}
