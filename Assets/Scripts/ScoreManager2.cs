using UnityEngine;
using TMPro;  // Import TextMeshPro namespace

public class ScoreManager2 : MonoBehaviour
{
    // Score variable
    private int score = 0;

    // Reference to the TextMeshPro component to display the score
    public TextMeshPro scoreText;

    // Optional: Sound effect when scoring
    public AudioSource scoreSound;

    // Start is called before the first frame update
    void Start()
    {
        // Make sure to initialize the score display
        UpdateScoreDisplay();
    }

    // Method to call when the ball enters the hoop trigger
    public void ScorePoint()
    {
        // Increment the score
        score++;

        // Play a sound if a score sound is assigned
        if (scoreSound != null)
        {
            scoreSound.Play();
        }

        // Update the score display
        UpdateScoreDisplay();
    }

    // Method to update the score display
    void UpdateScoreDisplay()
    {
        // Display the current score in the 3D text
        scoreText.text = score.ToString();
    }
}