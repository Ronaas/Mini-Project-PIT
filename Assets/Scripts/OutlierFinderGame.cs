using System.Collections.Generic;
using UnityEngine;

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

    private void SetupGrid()
    {
        // Clear existing objects
        foreach (var obj in activeObjects)
        {
            Destroy(obj);
        }
        activeObjects.Clear();

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
        float startZ = wallCenter.z;                           // Align along the wall's Z-axis

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
                }
                else
                {
                    obj = Instantiate(identicalPrefab, position, Quaternion.identity);
                }

                // Rotate cards to face outward (toward the camera/player)
                obj.transform.rotation = Quaternion.Euler(0, 0, 0);

                activeObjects.Add(obj);
            }
        }
    }




    public void OnObjectHit(GameObject obj)
    {
        if (obj.CompareTag("Outlier"))
        {
            Debug.Log("Outlier hit! Destroying and resetting the game.");

            // Destroy the outlier and reset the grid
            Destroy(obj);
            SetupGrid();
        }
        else
        {
            Debug.Log("Wrong object hit!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            // Check if the bullet directly hits an outlier
            Collider bulletCollider = other;
            if (bulletCollider != null)
            {
                OnObjectHit(bulletCollider.gameObject);
            }

            // Destroy the bullet after collision
            Destroy(other.gameObject);
        }
    }




    // Method to handle when the outlier card is shot
    public void OutlierFound(GameObject outlier)
    {
        if (outlier == outlierPrefab)
        {
            Debug.Log("Outlier card found! Resetting the game.");
            
        }
        else
        {
            Debug.Log("Wrong card hit! Try again.");
        }
    }
}
