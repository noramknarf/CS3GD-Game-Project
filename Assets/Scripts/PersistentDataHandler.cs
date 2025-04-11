using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentDataHandler : MonoBehaviour
{
    public static PersistentDataHandler instance;
    public float remainingTime;
    public float[] personalBests;
    //I am making this an array so that the specific level's PB can be accessed dynamically by passing the same script different parameters, rather than needing a separate script for each level
    public float currentTotalScore;

    void Awake(){
    if(instance != null){
        Destroy(gameObject);
        
        return;
    }
        personalBests = new float[2]; 
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        Debug.Log("number of levels in the game:" + personalBests.Length);
    }
}
