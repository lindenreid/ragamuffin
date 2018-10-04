using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class LevelBuilder : EditorWindow {

	private string _titleString = "Level Builder";
	private string _assetPath = "Assets/Prefabs/Environment";

	private List<string> _assets;
	private GameObject _activeObj;

	private Grid grid;

	[MenuItem ("Window/Level Builder")]
	public static void ShowWindow()
	{
		EditorWindow.GetWindow(typeof(LevelBuilder));
	}

	public void OnEnable()
	{
		GameObject gridObj = (GameObject)GameObject.Find("Grid");
		if(gridObj)
			grid = gridObj.GetComponent<Grid>();
		else 
			Debug.LogError("LevelBuilder could not find Grid!");

		_assets = new List<string>();
		// subscribes OnSceneUpdate to scene view update,
		// so that we can listen for mouse clicks in scene view
		SceneView.onSceneGUIDelegate = OnSceneUpdate;
	}

	public void OnSceneUpdate(SceneView sceneView){
		Event e = Event.current; // mouse/key events

		if(e.isMouse && e.button == 0 && e.clickCount == 1 && _activeObj)
		{
			//Debug.Log("active object: " + _activeObj.name);

			// get world-space mouse position
			Ray r = Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight));
			Vector3 mousePosition = r.origin;

			// snap position to closest grid location
			Vector3 closestGridPoint = grid.grid[0,0];
			foreach(Vector3 point in grid.grid)
			{
				if (Mathf.Abs(Vector3.Distance(point, mousePosition)) < Mathf.Abs(Vector3.Distance(closestGridPoint, mousePosition)))
					closestGridPoint = point;
			}
			closestGridPoint.z = 0;

			// instantiate object at mouse position
			Object obj = Object.Instantiate(_activeObj, closestGridPoint, Quaternion.identity);
		}
	}

	private void PopulateAssets(string path)
	{
		// find all prefabs in given directory
		DirectoryInfo dir = new DirectoryInfo(path);
        FileInfo[] info = dir.GetFiles("*.prefab");

		foreach (FileInfo file in info) {
			string asset = _assetPath + "/" + file.Name;
			if(!_assets.Contains(asset)){
				_assets.Add(asset);
				//Debug.Log("found asset " + asset);
			}
		}
	}

	// creates the LevelBuilder window
	void OnGUI()
	{
		PopulateAssets(_assetPath); //todo: make asset path editable, only reload assets on edit

		GUILayout.Label(_titleString, EditorStyles.boldLabel);

		if(GUILayout.Button("Clear Selected Object"))
		{
			ClearSelection();
		}

		// load all found assets into buttons
		// button click loads corresponding object into active object
		foreach(string path in _assets)
		{
			// find object 
			GameObject obj = (GameObject)AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));

			if(obj)
			{
				// get icon for this object
				Texture2D icon = AssetPreview.GetAssetPreview(obj);

				if(GUILayout.Button(icon))
				{
					_activeObj = obj;
				}
			}
			else 
			{
				Debug.LogError("LevelBuilder unable to find corresponding object for asset at" + path);
			}
		}
	}

	void OnDestroy()
	{
		// clear our selected object when we close
		ClearSelection();
	}

	private void ClearSelection()
	{
		// todo: maybe have highlighting or something to indicate selection
		// that will be cleared here later
		_activeObj = null;
	}

}