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

        // Determine grid start position (top-left corner on the front face)
        float startX = wallCenter.x - (wallSize.x / 2) + (wallSize.x - (gridColumns - 1) * spacing) / 2;
        float startY = wallCenter.y + (wallSize.y / 2) - (wallSize.y - (gridRows - 1) * spacing) / 2;
        float startZ = wallCenter.z + (wallSize.z / 2); // Front face of the cube

        int outlierIndex = Random.Range(0, gridRows * gridColumns);

        // Generate the grid
        for (int row = 0; row < gridRows; row++)
        {
            for (int col = 0; col < gridColumns; col++)
            {
                // Calculate position
                Vector3 position = new Vector3(
                    startX + col * spacing,
                    startY - row * spacing,
                    startZ
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

                // Make sure objects face outward
                obj.transform.LookAt(obj.transform.position - Vector3.forward);

                activeObjects.Add(obj);
            }
        }
    }

    public void OnObjectHit(GameObject obj)
    {
        if (obj.CompareTag("Outlier"))
        {
            // Outlier is hit: Remove it and reset the game
            Destroy(obj);
            SetupGrid();
        }
        else
        {
            // Identical object is hit: Optionally, provide feedback or ignore
            Debug.Log("Wrong object!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the bullet hits any object in the scene
        if (other.CompareTag("Bullet"))
        {
            RaycastHit hit;
            if (Physics.Raycast(other.transform.position, other.transform.forward, out hit))
            {
                if (hit.collider != null)
                {
                    OnObjectHit(hit.collider.gameObject);
                }
            }
        }
    }
}
