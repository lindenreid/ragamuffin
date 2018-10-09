using UnityEngine;

// PLEASE keep giant, powerful classes like this sparse
// logic should REALLY go in its own system
// and this class should mainly be for accessing things
// that almost every object will need to access
public class GameController : MonoBehaviour {

	public float gravity = -25.0f;

	public MainCharacter mainCharacter;

}
