using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemDustRingManager : MonoBehaviour {

    private float init_time;

	// Use this for initialization
	void Start () {
        init_time = Time.timeSinceLevelLoad;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - init_time >= 2.5f)
        {
            Destroy(gameObject);
        }
	}

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("called");

        if(other.tag == "Enemy")
        {
            Debug.Log("Dust Ring Hit");
        }
    }
}
