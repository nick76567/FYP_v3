using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemSmokeParticleManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("called1");

        if (other.tag == "Enemy")
        {
            Debug.Log("Dust Hit");
        }
    }
}
