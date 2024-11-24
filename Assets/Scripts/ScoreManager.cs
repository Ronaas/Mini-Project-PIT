using UnityEngine;
using TMPro;  // Import the TextMeshPro namespace

public class ScoreManager : MonoBehaviour
{
    public int score = 0;  // Variable to keep track of the score
    public TextMeshPro scoreText;  // Reference to the TextMeshPro object for displaying score

    void Start()
    {
        // Initialize the score text in the 3D world
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    // Method to update the score
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    // Method to update the displayed score text
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }
}
