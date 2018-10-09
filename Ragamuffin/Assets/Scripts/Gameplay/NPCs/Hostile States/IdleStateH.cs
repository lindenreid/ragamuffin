using UnityEngine;

public class IdleStateH : HostileState {

	public IdleStateH(Hostile e) : base(e) { name = "idle (Hostile)"; }

	public override void Update()
	{
		if(!owner.controller.isGrounded)
		{
			ApplyVelocityY();
			Move();
		}
	}

}