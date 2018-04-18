using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

	public Slider slider;
	public AudioSource music;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (SceneManager.GetActiveScene().name);
		if (SceneManager.GetActiveScene().name == "New Menu") {
			
			music.volume = slider.value;
		}
	}
}
