using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TrainController : MonoBehaviour
{
    public GameObject destinationPoint;

    // Start is called before the first frame update
    void Start()
    {
        if (destinationPoint == null){
            Debug.LogError(name + " is a moving object and requires a destination to move to.");
        }
        else{
            MoveToDestiniation();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MoveToDestiniation(){
        transform.DOMove(new Vector3(Random.Range(-4,4), transform.position.y, Random.Range(-4,4)), 2);
    }


}
