using UnityEngine;
using System.Collections.Generic;

public class OutlierFinderGame : MonoBehaviour
{
    public GameObject identicalPrefab; // Prefab for identical objects
    public GameObject outlierPrefab;   // Prefab for the outlier
    public GameObject wall;           // Reference to the wall (cube) in the scene
    public int gridRows = 3;          // Number of rows in the grid
    public int gridColumns = 3;       // Number of columns in the grid
    public float spacing = 0.5f;      // Spacing between objects on the grid

    private List<GameObject> activeObjects = new List<GameObject>();

    private void Start()
    {
        SetupGrid();
    }

    public void SetupGrid()
    {
        ClearGrid(); // Clear any existing grid before setting up a new one

        // Get wall dimensions
        Renderer wallRenderer = wall.GetComponent<Renderer>();
        Vector3 wallSize = wallRenderer.bounds.size;
        Vector3 wallCenter = wallRenderer.bounds.center;

        // Calculate grid's start position (top-left corner)
        float gridWidth = (gridColumns - 1) * spacing; // Total grid width
        float gridHeight = (gridRows - 1) * spacing;   // Total grid height

        // Starting positions to center grid on the -X face
        float startX = wallCenter.x - (wallSize.x / 2) - 0.01f; // Slightly offset to the -X face
        float startY = wallCenter.y + gridHeight / 2;          // Center vertically on Y-axis
        float startZ = wallCenter.z + -1;                           // Align along the wall's Z-axis

        // Randomly choose the outlier position
        int outlierIndex = Random.Range(0, gridRows * gridColumns);

        // Generate grid
        for (int row = 0; row < gridRows; row++)
        {
            for (int col = 0; col < gridColumns; col++)
            {
                // Calculate position for each card
                Vector3 position = new Vector3(
                    startX,                     // Aligned to -X face
                    startY - row * spacing,     // Vertical positioning along Y-axis
                    startZ + col * spacing      // Horizontal positioning along Z-axis
                );

                // Instantiate prefab
                GameObject obj;
                if (activeObjects.Count == outlierIndex)
                {
                    obj = Instantiate(outlierPrefab, position, Quaternion.identity);
                    obj.tag = "Outlier"; // Ensure correct tag for the outlier
                }
                else
                {
                    obj = Instantiate(identicalPrefab, position, Quaternion.identity);
                    obj.tag = "Identical"; // Optionally tag identical objects for better debugging
                }

                // Rotate cards to face outward (toward the camera/player)
                obj.transform.rotation = Quaternion.Euler(0, 0, 0);

                // Add the new object to the activeObjects list
                activeObjects.Add(obj);
                Debug.Log($"Added object to grid: {obj.name}");
            }
        }
    }

    public void ClearGrid()
    {
        // Destroy all objects in the activeObjects list
        foreach (var obj in activeObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        activeObjects.Clear();

        // Fallback: Destroy any remaining objects by tag
        foreach (var obj in GameObject.FindGameObjectsWithTag("Identical"))
        {
            Destroy(obj);
        }

        foreach (var obj in GameObject.FindGameObjectsWithTag("Outlier"))
        {
            Destroy(obj);
        }

        Debug.Log("Grid cleared.");
    }
    public void ResetGame()
    {
        Debug.Log("Resetting the game...");
        ClearGrid();
        SetupGrid();
    }
}
