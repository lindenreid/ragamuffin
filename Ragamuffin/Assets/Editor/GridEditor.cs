using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(Grid))]
public class GridEditor : Editor {

    Grid grid;

    public void OnEnable()
    {
        grid = (Grid)target;
    }

    public override void OnInspectorGUI()
    { 
        GUILayout.BeginVertical();

        GUILayout.Label("Start X");
        grid.StartX = EditorGUILayout.FloatField(grid.StartX, GUILayout.Width(50));

        GUILayout.Label("Start Y");
        grid.StartY = EditorGUILayout.FloatField(grid.StartY, GUILayout.Width(50));

        GUILayout.Label("Cell Width");
        grid.CellWidth = EditorGUILayout.FloatField(grid.CellWidth, GUILayout.Width(50));

        GUILayout.Label("Cell Height");
        grid.CellHeight = EditorGUILayout.FloatField(grid.CellHeight, GUILayout.Width(50));

        GUILayout.Label("Number of cells in x dir"); 
        grid.NumCellsX = EditorGUILayout.IntField(grid.NumCellsX, GUILayout.Width(50));

        GUILayout.Label("Number of cells in y dir");
        grid.NumCellsY = EditorGUILayout.IntField(grid.NumCellsY, GUILayout.Width(50));

        GUILayout.Label("Icon size");
        grid.IconSize = EditorGUILayout.FloatField(grid.IconSize, GUILayout.Width(50));
        

        GUILayout.EndVertical();

        SceneView.RepaintAll();
    }

}
