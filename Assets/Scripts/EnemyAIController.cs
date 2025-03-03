using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//for now just implementing the tutorial waypoint-based patrol logic so just follow that for now.

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

    void Idle(){

    }
}
