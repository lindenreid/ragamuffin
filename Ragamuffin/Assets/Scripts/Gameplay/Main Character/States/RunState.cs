using UnityEngine;

public class RunState : MovementState {

	public RunState(MainCharacter c) : base(c) { name = "run"; }

	public override void Update()
	{
		float horizontalInput = Input.GetAxis(mc.horizontalAxis);

		if(Mathf.Abs(horizontalInput) < mc.xInputThreshold)
		{
			mc.SetMovementState(mc.idleState);
		}
		else if(mc.controller.isGrounded && Input.GetAxis(mc.verticalAxis) > 0)
        {
            mc.SetMovementState(mc.jumpState);
        }
		else
		{
			ApplyGrounding();
			ApplyVelocityX(mc.groundDamping);
            ApplyVelocityY();
			Move();
		}
	}

}
