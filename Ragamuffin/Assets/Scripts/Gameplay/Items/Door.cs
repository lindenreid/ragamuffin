using UnityEngine;

public class Door : Interactable {

	public Sprite openSprite;
	public Sprite closedSprite;

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
