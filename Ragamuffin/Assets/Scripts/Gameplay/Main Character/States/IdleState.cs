using UnityEngine;

public class IdleState : MovementState {

    public IdleState(MainCharacter c) : base(c) { name = "idle"; }

	public override void Update()
	{
        if(mc.runState.StateAllowed())
        {
            mc.SetMovementState(mc.runState);
        }
        else if(mc.jumpState.StateAllowed())
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