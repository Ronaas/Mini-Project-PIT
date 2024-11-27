using UnityEngine;

public class BasketballScore : MonoBehaviour
{
    // Reference to the ScoreManager
    public ScoreManager2 scoreManager2;

    // Method to call when the ball enters the hoop trigger
    void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the hoop is the ball (using a tag or the object's name)
        if (other.CompareTag("Basket"))  // Make sure to tag the ball as "Basketball"
        {
            // Call ScoreManager to update the score
            scoreManager2.ScorePoint();
        }
    }
}