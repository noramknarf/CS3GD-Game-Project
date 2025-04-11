using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentDataHandler : MonoBehaviour
{
    public static PersistentDataHandler instance;
    public float remainingTime;

    void Awake(){
    if(instance != null){
        Destroy(gameObject);
        return;
    }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
