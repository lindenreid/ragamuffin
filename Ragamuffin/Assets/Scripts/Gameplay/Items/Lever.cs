using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Interactable {

	public Sprite onSprite;
	public Sprite offSprite;

	private SpriteRenderer spriteRenderer;
	private bool on;

	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		on = false;
	}

	public override void Interact()
	{
		if(on)
		{
			on = false;
			spriteRenderer.sprite = offSprite;
		}
		else 
		{
			on = true;
			spriteRenderer.sprite = onSprite;
		}

		foreach(Interactable i in triggers)
		{
			i.Trigger();
		}
	}
	
}
