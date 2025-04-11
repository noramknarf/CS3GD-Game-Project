using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantDataHandler : MonoBehaviour
{
    public static PersistantDataHandler instance;

    void Awake(){
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
