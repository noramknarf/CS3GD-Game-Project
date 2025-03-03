using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//current task is to implement chase logic. Target player as destination if within a certain distance.

public class EnemyAIController : MonoBehaviour
{

    public enum AgentState{
    Idle = 0, patroling, chasing
}

public AgentState state;
public NavMeshAgent agent;
public Transform player;
public LayerMask whatIsGround;
public LayerMask whatIsPlayer;

//patrol variables
public Transform[] waypoints;
private int index = 0;
[SerializeField]
private float distToSwitchWaypoints = 1.0f;

//Chase variables
[SerializeField]
private float detectionRange = 15.0f;
private bool playerInDetectionRange;



    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        if (state == AgentState.patroling){
            Patrol();
        }
        else if (state == AgentState.chasing){
            Chase();
        }
        else{
            Idle();
        }
        
    }

    //Has the enemy cycle through a set of pre-made waypoints and wander between them.
    void Patrol(){
        agent.isStopped = false;
        if (agent.remainingDistance <= distToSwitchWaypoints){
            agent.SetDestination(waypoints[index].position);
            index = (index + 1)% waypoints.Length;
        }
    }

    void Chase(){
        playerInDetectionRange = Physics.CheckSphere(this.transform.position, detectionRange, whatIsPlayer);
        print(playerInDetectionRange);

        if (playerInDetectionRange){
            agent.SetDestination(player.position);
        }
        else{
            Idle();
        }
    }

    void Idle(){

    }
}
