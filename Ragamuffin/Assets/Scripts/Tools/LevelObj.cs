using UnityEngine;

[ExecuteInEditMode]
public class LevelObj : MonoBehaviour {

	public Grid grid;
	public bool freePlace = false;

	private Vector3 oldPosition;

	void Start()
	{
		oldPosition = transform.position;
	}

	void Update()
	{
		if(!freePlace)
		{
			if(oldPosition != transform.position && grid)
			{
				transform.position = grid.FindClosestGridPos(transform.position);
			}
			oldPosition = transform.position;
		}
	}
	
}
