using UnityEngine;

[RequireComponent(typeof(LevelObj))]
public class Interactable : MonoBehaviour {

	public float InteractionRange = 1.0f;
	
	private LevelObj levelObj;
	private Transform mainCharacterTransform;

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
	
}
