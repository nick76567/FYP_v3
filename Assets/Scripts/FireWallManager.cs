using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWallManager : Photon.PunBehaviour {

    private float initTime;
    private int magicalAp;


    // Use this for initialization
    void Start () {
        initTime = Time.timeSinceLevelLoad;    
        magicalAp = 60;
    }
	
	// Update is called once per frame
	void Update () {
    
		if(Time.timeSinceLevelLoad - initTime > 2.0f)
        {
            Destroy(gameObject);
        }
    
	}

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("Water hit");
            other.GetComponent<CharacterAbility>().MagicalDamage(magicalAp);
        }
    }
}
