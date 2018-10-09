using UnityEngine;
using Prime31;

[RequireComponent(typeof(CharacterController2D))]
public class Hostile : MonoBehaviour {

	// controllers
	public GameController gameController;

	// movement config
	public float patrolSpeed = 8f;
	public float groundDamping = 20f; // how fast do we change direction? higher means faster
	
	// movement states
	public HostileState currentState;
	public IdleStateH idleState;
	public PatrolStateH patrolState;

	// movement vars
	public Vector3 velocity;

	[HideInInspector]
	public CharacterController2D controller;

	void Awake()
	{
		idleState = new IdleStateH(this);
		patrolState = new PatrolStateH(this);

		controller = GetComponent<CharacterController2D>();

		SetState(idleState);
	}

	void Update()
	{
		if(currentState != null)
			currentState.Update();
	}

	public void SetState(HostileState newState)
	{
		if(currentState != null)
			currentState.OnStateExit();

		currentState = newState;

		if(currentState != null)
			currentState.OnStateEnter();

		//Debug.Log("new Hostile state: " + currentState.name);
	}

}
