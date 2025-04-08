using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TrainController : MonoBehaviour
{
    public GameObject destinationPoint;
    public float travelTime = 7.0f;

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
        transform.DOMove(destinationPoint.transform.position, travelTime).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }


}
