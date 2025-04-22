using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TrainController : MonoBehaviour
{
    public GameObject destinationPoint;
    public float travelTime = 20.0f;
    public Transform player;
    private float startX;
    private Vector3 startpoint;
    private CharacterMover mover;
    private Vector3 trainV;

    private CharacterController playerController;

    // Start is called before the first frame update
    void Start()
    {
        if (destinationPoint == null){
            Debug.LogError(name + " is a moving object and requires a destination to move to.");
        }
        else{
            startX = this.transform.position.x;
            startpoint = new Vector3(startX, this.transform.position.y, this.transform.position.z);
            trainV =  (destinationPoint.transform.position - startpoint)/travelTime; //s = d/t. dest vector - start vector gives a direction multiplied by magnitude, right?

            if (player == null){
                Debug.Log("Player variable not set. Attempting to locate player Tag");
                player = GameObject.FindWithTag("Player").transform;
                if (player != null){
                    Debug.Log("Successfully found an object with the player tag");
                    playerController = player.GetComponent<CharacterController>();
                    mover = player.GetComponent<CharacterMover>();
                }
                else{
                    Debug.LogError("Error: " + name + " has no set target for the player variable and none could be located within the scene.");
                }
            }
            else{
                playerController = player.GetComponent<CharacterController>();
                mover = player.GetComponent<CharacterMover>();
            }

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

   /* void OnTriggerStay(Collider other){
        // two possible ways to go about keeping the player on the train: 
        // simply move the character by an amount that keeps them on top of the train as it moves,
        // OR fiddle with the charactermover class so we can get the char's velocity and adjust that.
        if (other.tag == "Player"){
            Vector3 playerMoveDirection = mover.GetMoveDirection();
            Debug.Log(playerMoveDirection);
            playerMoveDirection += trainV;
            Debug.Log(playerMoveDirection);
            mover.SetMoveDirection(playerMoveDirection); //works but since it repeats every frame, it causes the player to accelerate infinitely. Maybe clamp somehow?
            //perhaps clamp to the speed of the train plus the player's max walking velocity.
        }

    }*/


}
