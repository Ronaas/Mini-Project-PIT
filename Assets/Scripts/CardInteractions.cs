using UnityEngine;

public class CardInteractions : MonoBehaviour
{
    public GameObject outlierGameObject;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Notify the game manager (OutlierFinderGame) to reset
            NotifyOutlierHit();

            // Destroy the outlier
            Destroy (GameObject.FindWithTag("Card"));
        }
    }

    void NotifyOutlierHit()
    {
        OutlierFinderGame outlierFinder = outlierGameObject.GetComponent<OutlierFinderGame>();

        if (outlierFinder != null)
        {
            Debug.Log("Outlier hit! Notifying game manager to reset.");
            outlierFinder.ResetGame();
        }
        else
        {
            Debug.LogError("OutlierFinderGame component not found on " + outlierGameObject.name);
        }
    }
}