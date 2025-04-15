using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentDataHandler : MonoBehaviour
{
    public static PersistentDataHandler instance;
    public float remainingTime = 0;
    public float[] personalBests;
    //I am making this an array so that the specific level's PB can be accessed dynamically by passing the same script different parameters, rather than needing a separate script for each level
    public float currentTotalScore;

    public const string APPLICATION_ID = "022A73B8-8106-45AB-8A3E-13C35A048C67"
    public const string REST_SECRET_KEY = "3523EAD7-33DC-4F93-9443-CAD91086CEDA";

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
