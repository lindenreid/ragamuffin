using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class LevelBuilder : EditorWindow {

	private string _titleString = "Level Builder";
	private string _assetPath = "Assets/Prefabs/Environment";
	private Vector2 _scrollPos;

	// putting the assets and the directories in structs is maybe overkill,
	// but in the future i'd like to add tags to the assets
	// to make them searchable by more than just name,
	// which this struct will be useful for! :D 
	private struct AssetObj
	{
		public string name;
		public GameObject obj;
		public Texture2D icon;

		public AssetObj(string path)
		{
			// find object 
			obj = (GameObject)AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));

			if(obj)
			{
				name = obj.name;
				icon = AssetPreview.GetAssetPreview(obj);
			}
			else 
			{
				// it's ok, unity's gui won't crash from trying to access null stuff
				// teehee i'm lazy
				Debug.LogError("LevelBuilder unable to find object at path " + path);
				name = "ERROR";
				icon = null;
			}
		}
	}

	private struct AssetDir
	{
		public string name;
		public List<AssetObj> assets;

		public AssetDir(string n) {
			name = n;
			assets = new List<AssetObj>();
		}
	}

	private List<AssetDir> _assets;
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

		PopulateAssets(_assetPath);

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
		_assets = new List<AssetDir>();
		DirectoryInfo dir = new DirectoryInfo(path);

		// find all directories
		// NOTE: ONLY GOES ONE LEVEL DEEP. don't rlly feel like making this recursive
		DirectoryInfo[] subDirs = dir.GetDirectories();

		foreach(DirectoryInfo d in subDirs)
		{
			AssetDir assetDir = new AssetDir(d.Name);

			// find all prefabs in each directory
			FileInfo[] info = d.GetFiles("*.prefab");
			foreach (FileInfo file in info) {
				string assetPath = _assetPath + "/" + assetDir.name + "/" + file.Name;
				assetDir.assets.Add(new AssetObj(assetPath));
			}

			_assets.Add(assetDir);
		}
	}

	// creates the LevelBuilder window
	void OnGUI()
	{
		GUILayout.Label(_titleString, EditorStyles.boldLabel);
		if(GUILayout.Button("Clear Selected Object"))
		{
			ClearSelection();
		}
		EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
		
		// SCROLL
		var window = EditorWindow.GetWindow<LevelBuilder>();
		_scrollPos = GUILayout.BeginScrollView(_scrollPos, GUILayout.Width(window.position.width), GUILayout.Height(window.position.height-70));

		// create lists for each directory
		for (int i = 0; i < _assets.Count; i++)
		{
			AssetDir dir = _assets[i]; 

			GUILayout.Label(dir.name, EditorStyles.boldLabel);
			
			// load all found assets into buttons
			// button click loads corresponding object into active object
			GUILayout.BeginHorizontal();
			foreach(AssetObj ao in dir.assets)
			{
				GUILayout.BeginVertical();
				GUILayout.Label(ao.name, EditorStyles.label);

				// highlight button for active object
				if(_activeObj == ao.obj)
				{
					GUI.backgroundColor = Color.blue;
				}
				else
				{
					GUI.backgroundColor = Color.white;
				}

				if(GUILayout.Button(ao.icon, GUILayout.Width(ao.icon.width), GUILayout.Height(ao.icon.height)))
				{
					_activeObj = ao.obj;
				}
				GUILayout.EndVertical();
			}
			GUILayout.EndHorizontal();
		}

		GUILayout.EndScrollView();
		// END SCROLL
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