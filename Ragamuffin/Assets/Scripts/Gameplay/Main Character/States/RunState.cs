﻿using UnityEngine;

public class RunState : MovementState {

	public RunState(MainCharacter c) : base(c) { name = "run"; }

	public override void Update()
	{
		if(mc.stealthState.ConditionsMet())
		{
			mc.SetMovementState(mc.stealthState);
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
			ApplyVelocityX(mc.runSpeed, mc.groundDamping);
            ApplyVelocityY();
			Move();
		}
	}

	public override bool ConditionsMet()
	{
		if(Input.GetKey(KeyCode.LeftShift) && 
			Mathf.Abs(Input.GetAxis(mc.horizontalAxis)) > mc.xInputThreshold)
			return true;
		return false;
	}

}
