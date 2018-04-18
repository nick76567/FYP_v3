using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyMusic : MonoBehaviour {

	static DontDestroyMusic instance = null;
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
	
	// Update is called once per frame
	void Update () {
		
	}
}
