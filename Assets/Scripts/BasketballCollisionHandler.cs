using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketballCollionHandler : MonoBehaviour
{

    private ScoreManager scoreManager1;  // Reference to the ScoreManager

    private void Start()
    {
        // Find the ScoreManager in the scene
        scoreManager1 = FindObjectOfType<ScoreManager>();
    }

    // This method is called when another collider enters this cup's collider
    void OnTriggerEnter(Collider other)
    {
        // Check if the object that collided with the cup has the tag "Bullet"
        if (other.gameObject.CompareTag("Basket"))
        {

            // Update the score in the ScoreManager
            if (scoreManager1 != null)
            {
                scoreManager1.AddScore(1);
            }


            
        }
    }
}
