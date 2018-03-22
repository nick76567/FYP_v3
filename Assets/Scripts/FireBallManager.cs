using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallManager : Photon.PunBehaviour {

    private float initTime;
    private int magicalAp;
    

    // Use this for initialization
    void Start () {
        
        initTime = Time.timeSinceLevelLoad;
        
        magicalAp = 100;
    }
	
	// Update is called once per frame
	void Update () {
        
        
		if(Time.timeSinceLevelLoad - initTime >= 2f)
        {
            
            Destroy(gameObject);
        }
        
	}

    private void OnDestroy()
    {
        Debug.Log("Fire ball is destoried");
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.tag == "Enemy")
        {
            Debug.Log("Hit");
            other.GetComponent<CharacterAbility>().MagicalDamage(magicalAp);
            Destroy(gameObject);
        }
    }

    
}
