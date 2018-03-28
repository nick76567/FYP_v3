using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BuySword()
    {
        Sword sword = new Sword();
        Debug.Log("Sword pap " + sword.GetPapIncreaseRate());
        Debug.Log("Sword speed " + sword.GetpSpeedIncreaseRate());
    }
}
