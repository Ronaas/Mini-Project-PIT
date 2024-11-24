using UnityEngine;

public class CupCollisionHandler : MonoBehaviour
{

    private ScoreManager scoreManager;  // Reference to the ScoreManager

    private void Start()
    {
        // Find the ScoreManager in the scene
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    // This method is called when another collider enters this cup's collider
    void OnCollisionEnter(Collision collision)
    {
        // Check if the object that collided with the cup has the tag "Bullet"
        if (collision.gameObject.CompareTag("Bullet"))
        {
  
            // Update the score in the ScoreManager
            if (scoreManager != null)
            {
                scoreManager.AddScore(1);
            }


            // Destroy the cup after it is hit
            Destroy(gameObject);  // This removes the cup after it is hit
        }
    }
}
