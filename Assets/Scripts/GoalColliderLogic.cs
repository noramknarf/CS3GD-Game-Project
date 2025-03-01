using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalColliderLogic : MonoBehaviour
{
    [SerializeField]
    private AudioSource GoalSoundSource;

    /* Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */

    public void OnTriggerEnter(Collider triggeringObj){
        if (triggeringObj.tag == "Player"){
            print("Goal triggered");

        }

    }
}   
