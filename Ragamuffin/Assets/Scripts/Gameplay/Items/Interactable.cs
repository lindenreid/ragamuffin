using UnityEngine;
using System.Collections.Generic;

// Interact() is used for player interactions with object,
// while Trigger() is used for other Interactables triggering it.
// For example, a locked door might simply display "locked!" when Interact()ed with,
// but upon pulling a hidden lever, which calls Trigger(), it opens.

[RequireComponent(typeof(LevelObj))]
public class Interactable : MonoBehaviour {

	public float InteractionRange = 1.0f;
	public List<Interactable> triggers;
	
	public LevelObj levelObj;
	public Transform mainCharacterTransform;

	void Awake()
	{
		levelObj = GetComponent<LevelObj>();
		if(levelObj && levelObj.gameController)
			mainCharacterTransform = levelObj.gameController.mainCharacter.transform;
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space) && mainCharacterTransform && Vector3.Distance(transform.position, mainCharacterTransform.position) <= InteractionRange)
			Interact();
	}

	public virtual void Interact()
	{
		Debug.LogWarning(gameObject.name + " Iteractable's Interact() funciton not implemented.");
	}

	public virtual void Trigger()
	{
		Debug.LogWarning(gameObject.name + " Iteractable's Trigger() funciton not implemented.");
	}
	
}
