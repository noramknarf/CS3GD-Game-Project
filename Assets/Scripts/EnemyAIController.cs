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
public AudioSource growlSFX;

//patrol variables
public Transform[] waypoints;
private int index = 0;
[SerializeField]
private float distToSwitchWaypoints = 2.0f;

//Chase variables
[SerializeField]
private float detectionRange = 15.0f;
private bool playerInDetectionRange;

//Charge attack variables
public float attackRange = 5.0f;
public float attackCooldown = 5.0f;
public float attackWindup = 2.0f;
public float distToBeginBite = 1.5f;

    
private float windupTimer = 0.0f;
private float timeSinceAttack = 0.0f;
private bool attackReady = true;
private bool preparingAttack = false;
private bool playerInAttackRange = false;
private bool charging = false;
private Animator enemyAnimator;
private int movementStateHash = Animator.StringToHash("MovementState");
private int attackStateHash = Animator.StringToHash("Attacking");


    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyAnimator = this.GetComponent<Animator>();
        if (player == null){
            Debug.Log("Player variable not set. Attempting to locate player Tag");
            player = GameObject.FindWithTag("Player").transform;
            if (player != null){
                Debug.Log("Successfully found an object with the player tag");
            }
            else{
                Debug.LogError("Error: " + name + " has no set target for the player variable and none could be located within the scene.");
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (state == AgentState.patroling){
            
            Patrol();
            //Debug.Log("patroling");
        }
        else if (state == AgentState.chasing){
            
            Chase();
        }
        else if(state == AgentState.attacking){
            Attack();
        }
        else{
            
            Idle();
        }
        
    }

    //Has the enemy cycle through a set of pre-made waypoints and wander between them.
    void Patrol(){
        enemyAnimator.SetInteger(movementStateHash, 2); //activate walk cycle
        agent.isStopped = false;
        if (agent.remainingDistance <= distToSwitchWaypoints){
            agent.SetDestination(waypoints[index].position);
            index = (index + 1)% waypoints.Length;
            Debug.Log("index =" + index);
        }
    }

    void Chase(){
        enemyAnimator.SetInteger(movementStateHash, 0); // set enemy to idle animation by default
        if(attackReady){
           // Debug.Log("checking if player is in range");
            playerInAttackRange = Physics.CheckSphere(this.transform.position, attackRange, whatIsPlayer);
            if (playerInAttackRange){
                state = AgentState.attacking;
            //    Debug.Log("Player within attack range");
            }
            else{
            //    Debug.Log("Player outside attack range");
                playerInDetectionRange = Physics.CheckSphere(this.transform.position, detectionRange, whatIsPlayer);
                if(playerInDetectionRange){
                 //   Debug.Log("Player within detection range, moving towards player");
                    enemyAnimator.SetInteger(movementStateHash, 2); //activate walk cycle if in pursuit
                    agent.isStopped = false;
                    agent.SetDestination(player.position);

                }
            }
        }
        else {
            timeSinceAttack += Time.deltaTime;
           // Debug.Log("Waiting on attack cooldown. time remaining: " + (attackCooldown - timeSinceAttack));
            if (timeSinceAttack >= attackCooldown){
                attackReady = true;
            }
        }

        //

    }

    void Attack() {
        if (!charging) {
            attackReady = false;
            preparingAttack = true;
        }
        
        if (preparingAttack) {
            windupTimer += Time.deltaTime;
            agent.isStopped = true;
            enemyAnimator.SetInteger(movementStateHash, 1); // activate neutral state animation
            transform.LookAt(new Vector3(player.position.x, 0.0f, player.position.z));
        }

        if (windupTimer >= attackWindup)
        {
            charging = true;
            enemyAnimator.SetInteger(movementStateHash, 3);
            preparingAttack = false;
           
            
            if (agent.isStopped) {
                float playerX = (float)player.position.x;
                Vector3 targetCoords = new Vector3(playerX, player.position.y, player.position.z);
                agent.isStopped = false;
                agent.SetDestination(targetCoords);
                Debug.Log("This should only happen once");
                growlSFX.Play();
            }

            Debug.Log("charge target = " + agent.destination);
            if (agent.remainingDistance <= distToBeginBite) {
                enemyAnimator.SetTrigger("Attacking");
                
                
                
            }
            if (agent.remainingDistance <= 1) {
                timeSinceAttack = 0.0f;
                charging = false;
                state = AgentState.chasing;
                Debug.Log("Reached target. Entering chase mode.");
                enemyAnimator.ResetTrigger("Attacking");
            }
            
        }



    }

    void Idle(){
        enemyAnimator.SetInteger(movementStateHash, 0);
    }
    /*enemy should chase until in attack range, then begin windup.
    while winding up, should freeze in place and face player.
    after a windup period, should take the player's location at time of the windup period ending and move quickly towards that point.
    once the point is reached, the enemy should reset and no longer be treated as attacking.
    Meanwhile, a cooldown begins for the next attack
    */
}
