using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CardInteraction : MonoBehaviour
{
  
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object collided with is a bullet
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Bullet hit card: " + gameObject.name);

            GameObject obj = GameObject.Find("OutlierGameManager"); // Replace "ObjectName" with the GameObject's name
            OutlierFinderGame targetScript = obj.GetComponent<OutlierFinderGame>();
            

            // Optionally, destroy the bullet upon collision
            Destroy(collision.gameObject);
        }
    }
}

