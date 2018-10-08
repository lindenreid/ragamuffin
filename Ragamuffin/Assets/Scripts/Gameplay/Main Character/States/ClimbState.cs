using UnityEngine;

public class ClimbState : MovementState {

	public ClimbState(MainCharacter c) : base(c) { name = "climb"; }

	public override void Update()
	{
		float horizontalInput = Input.GetAxis(mc.horizontalAxis);

		if(mc.controller.isGrounded || !mc.inClimbable)
		{
			mc.SetMovementState(mc.idleState);
		}
		else
		{
			ApplyGrounding();
			ApplyVelocityX(mc.climbSpeed, mc.climbDamping);
            ApplyClimb(); // ignores gravity
			Move();
		}
	}

	public override void OnStateEnter()
	{
		// ignores grounding for first movement calculation
		// to avoid problem where grounding checks prevent character
		// from lifting off the ground
		ApplyVelocityX(mc.climbSpeed, mc.climbDamping);
        ApplyClimb(); // ignores gravity
		Move();
	}

	public override bool ConditionsMet()
	{
		return false;
	}

}
