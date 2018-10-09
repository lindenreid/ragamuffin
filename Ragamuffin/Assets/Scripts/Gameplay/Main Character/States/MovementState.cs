using UnityEngine;

public abstract class MovementState {

	protected MainCharacter mc;
	public string name;

	protected float normalizedHorizontalSpeed = 0;
	protected float normalizedVerticalSpeed = 0;
	protected float gravity = 0.0f;

	public abstract void Update();

	public virtual bool ConditionsMet() { return true; }
	public virtual void OnStateEnter() {}
	public virtual void OnStateExit() {}
	public virtual void OnTriggerEnter(Collider2D col) {}
	public virtual void OnTriggerExit(Collider2D col) {}

	public MovementState(MainCharacter cm)
	{
		mc = cm;
		gravity = mc.gameController.gravity;
	}

	// does grounding check
	// separate from MoveWithInput so that Jump can ignore on first frame of jumping
	protected void ApplyGrounding()
	{
		if(mc.controller.isGrounded)
			mc.velocity.y = 0;
	}

	// changes x-velocity based on input & speed & damping
	protected void ApplyVelocityX(float speed, float damping)
	{
		// check horizontal input direction
		float horizontalInput = Input.GetAxis(mc.horizontalAxis);
		Vector3 localScale = mc.transform.localScale;
		if(horizontalInput < 0) // move left
		{
			normalizedHorizontalSpeed = -1;
			if(localScale.x > 0f)
				mc.transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
		}
		else if(horizontalInput > 0) // move right
		{
			normalizedHorizontalSpeed = 1;
			if(localScale.x < 0f)
				mc.transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
		}
		else
		{
			normalizedHorizontalSpeed = 0;
		}

		// apply horizontal speed
		mc.velocity.x = Mathf.Lerp(mc.velocity.x, normalizedHorizontalSpeed * speed, Time.deltaTime * damping);
	}

	// changes y-velocity based on input & gravity
	protected void ApplyVelocityY()
	{
		// apply gravity
		mc.velocity.y += gravity * Time.deltaTime;

		// if holding down bump up our movement amount and turn off one way platform detection for a frame.
		// this lets us jump down through one way platforms
		if(mc.controller.isGrounded && Input.GetAxis(mc.verticalAxis) < 0f)
		{
			mc.velocity.y *= 3f;
			mc.controller.ignoreOneWayPlatformsThisFrame = true;
		}
	}

	// applies vertical movement without gravity
	protected void ApplyClimb()
	{
		// check vertical input direction
		float verticalInput = Input.GetAxis(mc.verticalAxis);
		if(verticalInput < 0) // move down
			normalizedVerticalSpeed = -1;
		else if(verticalInput > 0) // move up
			normalizedVerticalSpeed = 1;
		else
			normalizedVerticalSpeed = 0;

		// apply vertical speed
		mc.velocity.y = Mathf.Lerp(mc.velocity.y, normalizedVerticalSpeed * mc.climbSpeed, Time.deltaTime * mc.climbDamping);
	}

	// applies movement to character controller
	protected void Move()
	{
		mc.controller.move(mc.velocity * Time.deltaTime);
		mc.velocity = mc.controller.velocity;
	}
	
}
