using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageHazardController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider triggeringObj){
        if (triggeringObj.tag == "Player"){
        SceneManager.LoadSceneAsync(2);
            
        }
    }
}
