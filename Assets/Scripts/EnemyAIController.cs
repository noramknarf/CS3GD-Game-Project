using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*Next to do is attack mechanics.
I'm thinking the enemy should stop a certain distance from the player, play a sound effect, then after a short time charge the player's loacation at the time the "windup" ended
*/

public class EnemyAIController : MonoBehaviour
{

    public enum AgentState{
    Idle = 0, patroling, chasing, attacking
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

//Charge attack variables
public float attackRange = 5.0f;
public float attackCooldown = 5.0f;
public float attackWindup = 2.0f;
private float timeSinceAttack = 0.0f;
private bool attackReady = true;
private bool preparingAttack = false;
private bool playerInAttackRange = false;



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
        else if(state == AgentState.attacking){
            agent.isStopped = true;
            print("Enemy would attack now");
            attackReady = false;
            timeSinceAttack = 0.0f;
            state = AgentState.chasing;
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
            
        if(attackReady){
            
            playerInAttackRange = Physics.CheckSphere(this.transform.position, attackRange, whatIsPlayer);
            if (playerInAttackRange){
                state = AgentState.attacking;
            }
            else{
                playerInDetectionRange = Physics.CheckSphere(this.transform.position, detectionRange, whatIsPlayer);
                if(playerInDetectionRange){
                    agent.isStopped = false;
                    agent.SetDestination(player.position);
                }
            }
        }
        else {
        timeSinceAttack += Time.deltaTime;
            if (timeSinceAttack >= attackCooldown){
                attackReady = true;
            }
        }

        //

    }

    void Idle(){

    }
    /*enemy should chase until in attack range, then begin windup.
    while winding up, should freeze in place and face player.
    after a windup period, should take the player's location at time of the windup period ending and move quickly towards that point.
    once the point is reached, the enemy should reset and no longer be treated as attacking.
    Meanwhile, a cooldown begins for the next attack
    */
}
