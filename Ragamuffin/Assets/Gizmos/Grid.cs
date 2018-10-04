using UnityEngine;

public class Grid : MonoBehaviour {

    public float StartX = 0.0f;
    public float StartY = 0.0f;

    public float CellWidth = 10.0f; 
    public float CellHeight = 10.0f;

    public int NumCellsX = 20;
    public int NumCellsY = 10;

    public float IconSize = 0.1f;

    public Vector3[,] grid;

    void CreateGrid()
    {
        grid = new Vector3[NumCellsX, NumCellsY];

        for (int x = 0; x < NumCellsX; x++) {
            for (int y = 0; y < NumCellsY; y++) {
                grid[x, y] = new Vector3(x * CellWidth + StartX, y * CellHeight + StartY, 0);
            }
        }
    }

    void OnDrawGizmos()
    {
        // todo: only re-create grid on change in inspector
        CreateGrid();

        for (int x = 0; x < NumCellsX; x++) {
            for (int y = 0; y < NumCellsY; y++) {
                Gizmos.DrawSphere(grid[x,y], IconSize);
            }
        }
    }

}
