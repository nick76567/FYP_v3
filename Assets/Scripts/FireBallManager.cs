using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallManager : MonoBehaviour {

    private float initTime;

	// Use this for initialization
	void Start () {
        initTime = Time.timeSinceLevelLoad;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - initTime >= 2f)
        {
            Destroy(gameObject);
        }
	}

    private void OnParticleCollision(GameObject other)
    {
        if(other.tag == "Enemy")
        {
            Debug.Log("Hit");
            Destroy(gameObject);
        }
    }

    
}
