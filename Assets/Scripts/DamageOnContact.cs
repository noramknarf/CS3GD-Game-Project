using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageOnContact : MonoBehaviour
{
    public CapsuleCollider hurtbox;
    private bool ableToDealDamage = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ToggleDamageON(){
        ableToDealDamage = true;
        Debug.Log("Hitbox on");
        Debug.Log(transform.gameObject.name);
    }
    void ToggleDamageOFF(){
        ableToDealDamage = false;
        Debug.Log("Hitbox off");
    }

    void OnTriggerStay(Collider other){
        Debug.Log("HIT SOMETHING");
        Debug.Log(ableToDealDamage);
        Debug.Log(other.transform.gameObject.name);
        if (other.tag == "Player" && ableToDealDamage ){
            Debug.Log("HIT PLAYER");
            SceneManager.LoadSceneAsync(2);
        }
    }
}
