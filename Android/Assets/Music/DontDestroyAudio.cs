using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyAudios : MonoBehaviour {

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
}
