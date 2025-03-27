using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageOnContact : MonoBehaviour
{
    public CapsuleCollider hurtbox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ToggleDamageON(){
        hurtbox.isTrigger = true;
        Debug.Log("Hitbox on");
        Debug.Log(transform.gameObject.name);
    }
    void ToggleDamageOFF(){
        hurtbox.isTrigger = false;
        Debug.Log("Hitbox off");
    }

    void OnTriggerEnter(Collider other){
        if (other.tag == "Player"){
            SceneManager.LoadSceneAsync(2);
        }
    }
}
