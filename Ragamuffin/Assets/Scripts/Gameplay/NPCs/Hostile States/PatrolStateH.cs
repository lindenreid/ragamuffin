using UnityEngine;

public class PatrolStateH : HostileState {

	public PatrolStateH(Hostile e) : base(e) { name = "patrol (Hostile)"; }

	public override void Update()
	{
        ApplyVelocityX(1, owner.patrolSpeed, owner.groundDamping);
		if(!owner.controller.isGrounded)
			ApplyVelocityY();
        Move();
	}

}