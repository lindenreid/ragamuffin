using UnityEngine;

public class RunState : MovementState {

	public RunState(MainCharacter c) : base(c) { name = "run"; }

	public override void Update()
	{
		if(!StateAllowed())
		{
			mc.SetMovementState(mc.idleState);
		}
		else if(mc.jumpState.StateAllowed())
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

	public override bool StateAllowed()
	{
		if(Mathf.Abs(Input.GetAxis(mc.horizontalAxis)) > mc.xInputThreshold)
			return true;
		return false;
	}

}
