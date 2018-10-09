using UnityEngine;

public abstract class HostileState {

    protected Hostile owner;
    public string name;

	protected float gravity = 0.0f;

    public abstract void Update();

    public virtual bool ConditionsMet() { return true; }
    public virtual void OnStateEnter() {}
	public virtual void OnStateExit() {}

    public HostileState(Hostile e)
    {
        owner = e;
        gravity = owner.gameController.gravity;
    }

    // applys x-velocity in given direction with given damping
    protected void ApplyVelocityX(float dir, float speed, float damping)
    {
        owner.velocity.x = Mathf.Lerp(owner.velocity.x, dir * speed, Time.deltaTime * damping);
    }

    // changes y-velocity based on gravity
	protected void ApplyVelocityY()
	{
        if(owner.controller.isGrounded)
			owner.velocity.y = 0;

		owner.velocity.y += gravity * Time.deltaTime;
	}

    protected void Move()
    {
        owner.controller.move(owner.velocity * Time.deltaTime);
		owner.velocity = owner.controller.velocity;
    }

}
