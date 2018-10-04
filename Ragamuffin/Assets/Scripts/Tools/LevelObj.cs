using UnityEngine;

[ExecuteInEditMode]
public class LevelObj : MonoBehaviour {

	public Grid grid;

	private Vector3 oldPosition;

	void Update()
	{
		if(oldPosition != transform.position)
		{
			transform.position = grid.FindClosestGridPos(transform.position);
		}
	}
	
}
