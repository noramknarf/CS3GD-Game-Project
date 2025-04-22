using UnityEngine;
using System.Collections;

public class CharacterMover : MonoBehaviour 
{
    //NOTICE: This code was initially copied over from the completed CS3GD lab 5 to be used as a basis. Credit for any logic remaining by the review stage goes to the relevant module tutor(s)
	public bool IsGrounded { get {return characterController.isGrounded;}}

	public float speed = 10.0f;
	public float jumpSpeed = 10.0f;
	public float gravity = 20.0f;
	public int maxJumps = 2;
	public float maxJumpCooldown = 0.2f;
	public float rotationSpeed = 100.0f;

	public float currentSpeed = 0.0f;
	public float maxSpeed = 10.0f;
	public float acceleration = 10.0f;
	public float decceleration = 20.0f;

	private Vector3 moveDirection = Vector3.zero;
	private CharacterController characterController;
	
	private int remainingJumps = 0;
	private float jumpCooldown;
	private float verticalVelocity = 0;

	private Animator playerAnimator;
	private int walkSpeedHash = Animator.StringToHash("playerMoveSpeed");
	private int verticalSpeedHash = Animator.StringToHash("playerVerticalSpeed");
	private int groundedHash = Animator.StringToHash("playerGrounded");
	private int turnSpeedHash = Animator.StringToHash("playerTurnSpeed");
	private int RemainingJumpHash = Animator.StringToHash("playerJumpsRemaining");
	private int airjumpHash = Animator.StringToHash("Airjumped");

	




	void Awake()
	{
		characterController = GetComponent<CharacterController>();
		jumpCooldown = maxJumpCooldown;
		playerAnimator = this.GetComponent<Animator>();
	}

	void Update() 
	{
		//decrement the cooldown timer(s)
		//jumpCooldown -= Time.deltaTime;
		jumpCooldown = Mathf.Clamp((jumpCooldown - Time.deltaTime), 0, maxJumpCooldown);

		// if we are on the ground then allow movement
		if (IsGrounded) 
	    {
			float input = Input.GetAxis("Vertical");
	        bool  isMoving = (input != 0);
			remainingJumps = maxJumps;
			playerAnimator.SetBool(airjumpHash, false);

			moveDirection.x = transform.forward.x;
			moveDirection.z = transform.forward.z;
	        
			if (isMoving) 
			{
				currentSpeed += ((acceleration * input) * Time.deltaTime);
				currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);
			}
			else if (currentSpeed > 0) 
			{
				currentSpeed -= (decceleration * Time.deltaTime);
				currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
			}
			else if (currentSpeed < 0) 
			{
				currentSpeed += (decceleration * Time.deltaTime);
				currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, 0);
			}

			moveDirection.x *= currentSpeed;
			moveDirection.z *= currentSpeed;

			moveDirection.y  = Mathf.Max(0, moveDirection.y);

	    }
		else
		{
			moveDirection.y -= (gravity * Time.deltaTime);
		}

		if (Input.GetButtonDown("Jump") && remainingJumps >= 1 && jumpCooldown == 0.0f)
		{
			moveDirection.y += jumpSpeed;
			remainingJumps--;
			jumpCooldown = maxJumpCooldown;
			print("Remaining jumps = " + remainingJumps + "/" + maxJumps);
			if(remainingJumps < 1){
				print("airjump!");
				playerAnimator.SetInteger(RemainingJumpHash, remainingJumps);
				print("Set remainingJumps to " + playerAnimator.GetInteger(RemainingJumpHash));
				playerAnimator.SetBool(airjumpHash, true);
			}
			
		}

		float rotation = (Input.GetAxis("Horizontal") * rotationSpeed) * Time.deltaTime;

	    transform.Rotate(0, rotation, 0);

		characterController.Move(moveDirection * Time.deltaTime);
		
		//update the playerAnimator parameters
					verticalVelocity = Mathf.Abs(moveDirection.y);
			playerAnimator.SetFloat(verticalSpeedHash, verticalVelocity);

		playerAnimator.SetBool(groundedHash , IsGrounded);
		playerAnimator.SetFloat(walkSpeedHash , currentSpeed);
		playerAnimator.SetFloat(turnSpeedHash , rotation);
		Debug.Log("turning " + rotation);
		//print("verticalSpeed = " + playerAnimator.GetFloat(verticalSpeedHash));
		//print("walkSpeed = " + playerAnimator.GetFloat(walkSpeedHash));
		
	}
	
}