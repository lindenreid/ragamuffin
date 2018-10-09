using UnityEngine;
using System.Collections;
using Prime31;

public class MainCharacter : MonoBehaviour
{
	// controllers
	public GameController gameController;

	// movement config
	public float runSpeed = 8f;
	public float stealthSpeed = 4f;
	public float groundDamping = 20f; // how fast do we change direction? higher means faster
	public float inAirDamping = 5f;
	public float climbDamping = 20f;
	public float jumpHeight = 3f;
	public float climbSpeed = 5f;
	public int climableLayer = 11;
	public string horizontalAxis = "Horizontal";
	public string verticalAxis = "Vertical";
	public float xInputThreshold = 0.01f;
	
	// movement states
	public MovementState currentMovementState;
	public JumpState jumpState;
	public RunState runState;
	public IdleState idleState;
	public ClimbState climbState;
	public StealthState stealthState;

	// movement vars
	public Vector3 velocity;
	public bool inClimbable;

	[HideInInspector]
	public CharacterController2D controller;
	//public Animator _animator;


	void Awake()
	{
		//_animator = GetComponent<Animator>();
		controller = GetComponent<CharacterController2D>();

		controller.onControllerCollidedEvent += onControllerCollider;
		controller.onTriggerEnterEvent += onTriggerEnterEvent;
		controller.onTriggerExitEvent += onTriggerExitEvent;

		// initialize all states
		jumpState = new JumpState(this);
		runState = new RunState(this);
		idleState = new IdleState(this);
		climbState = new ClimbState(this);
		stealthState = new StealthState(this);

		// set beginning state
		SetMovementState(idleState);
	}


	#region Event Listeners

	void onControllerCollider(RaycastHit2D hit)
	{
		// bail out on plain old ground hits cause they arent very interesting
		if( hit.normal.y == 1f )
			return;

		// logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
		//Debug.Log( "flags: " + controller.collisionState + ", hit.normal: " + hit.normal );
	}

	void onTriggerEnterEvent(Collider2D col)
	{
		//Debug.Log( "onTriggerEnterEvent: " + col.gameObject.layer );
		currentMovementState.OnTriggerEnter(col);
		if(col.gameObject.layer == climableLayer)
			inClimbable = true;
	}

	void onTriggerExitEvent(Collider2D col)
	{
		//Debug.Log( "onTriggerExitEvent: " + col.gameObject.layer );
		currentMovementState.OnTriggerExit(col);
		if(col.gameObject.layer == climableLayer)
			inClimbable = false;
	}

	#endregion

	void Update()
	{
		if(currentMovementState != null)
			currentMovementState.Update();
	}

	public void SetMovementState(MovementState newState)
	{
		if(currentMovementState != null)
			currentMovementState.OnStateExit();

		currentMovementState = newState;

		if(currentMovementState != null)
			currentMovementState.OnStateEnter();

		//Debug.Log("new state: " + currentMovementState.name);
	}

}
