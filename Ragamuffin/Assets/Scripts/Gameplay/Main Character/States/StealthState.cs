using UnityEngine;

public class StealthState : MovementState {

	public StealthState(MainCharacter c) : base(c) { name = "stealth"; }

	public override void Update()
	{
		if(mc.runState.ConditionsMet())
        {
            mc.SetMovementState(mc.runState);
        }
		else if(mc.jumpState.ConditionsMet())
        {
            mc.SetMovementState(mc.jumpState);
        }
        else if(!ConditionsMet())
		{
			mc.SetMovementState(mc.idleState);
		}
		else
		{
			ApplyGrounding();
			ApplyVelocityX(mc.stealthSpeed, mc.groundDamping);
            ApplyVelocityY();
			Move();
		}
	}

	public override bool ConditionsMet()
	{
		if(!Input.GetKey(KeyCode.LeftShift) && // shift is for running
			Mathf.Abs(Input.GetAxis(mc.horizontalAxis)) > mc.xInputThreshold)
			return true;
		return false;
	}

}