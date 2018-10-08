using UnityEngine;

public class IdleState : MovementState {

    public IdleState(MainCharacter c) : base(c) { name = "idle"; }

	public override void Update()
	{
        if(Mathf.Abs(Input.GetAxis(mc.horizontalAxis)) > mc.xInputThreshold)
        {
            mc.SetMovementState(mc.runState);
        }
        else if(mc.controller.isGrounded && Input.GetAxis(mc.verticalAxis) > 0)
        {
            mc.SetMovementState(mc.jumpState);
        }
        else 
        {
            // ignores x-axis movement input
            ApplyGrounding();
            ApplyVelocityY();
            Move();
        }
	}

    public override void OnStateEnter()
    {
        mc.velocity.x = 0f;
        mc.controller.velocity.x = 0f;
    }

}