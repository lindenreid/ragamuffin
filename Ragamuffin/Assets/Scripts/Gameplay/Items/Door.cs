using UnityEngine;

public class Door : Interactable {

	public Sprite openSprite;
	public Sprite closedSprite;
	public bool locked;

	private SpriteRenderer spriteRenderer;
	private BoxCollider2D boxCollider;

	private bool open;

	void Start()
	{
		open = false;
		spriteRenderer = GetComponent<SpriteRenderer>();
		boxCollider = GetComponent<BoxCollider2D>();
	}

	public override void Interact()
	{
		if(locked)
			Debug.Log("Locked!");
		else
			ToggleOpen();
	}

	public override void Trigger()
	{
		locked = false;
		ToggleOpen();
	}

	public void ToggleOpen()
	{
		if(open)
		{
			open = false;
			spriteRenderer.sprite = closedSprite;
			boxCollider.enabled = true; 
		}
		else 
		{
			open = true;
			spriteRenderer.sprite = openSprite;
			boxCollider.enabled = false;
		}
	}
	
}
