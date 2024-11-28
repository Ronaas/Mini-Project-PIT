using System.Collections.Generic;
using UnityEngine;

public class CardGameManager : MonoBehaviour
{
    public GameObject rightCardPrefab;
    public GameObject wrongCardPrefab;
    public Transform gridParent; // Parent object to organize cards
    private Vector3[] cardPositions;
    private List<int> availablePositions;

    void Start()
    {
        InitializePositions();
        SetupGrid();
    }

    void InitializePositions()
    {
        // Define a 3x3 grid
        cardPositions = new Vector3[9];
        int index = 0;
        for (int x = -1; x <= 1; x++)
        {
            for (int z = -1; z <= 1; z++)
            {
                cardPositions[index++] = new Vector3(x * 2, 0, z * 2);
            }
        }
    }

    void SetupGrid()
    {
        availablePositions = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8 };

        // Place the wrong card
        int wrongCardIndex = PickRandomPosition();
        Instantiate(wrongCardPrefab, cardPositions[wrongCardIndex], Quaternion.identity, gridParent);

        // Place the right cards
        for (int i = 0; i < 8; i++)
        {
            int rightCardIndex = PickRandomPosition();
            Instantiate(rightCardPrefab, cardPositions[rightCardIndex], Quaternion.identity, gridParent);
        }
    }

    int PickRandomPosition()
    {
        int randomIndex = Random.Range(0, availablePositions.Count);
        int positionIndex = availablePositions[randomIndex];
        availablePositions.RemoveAt(randomIndex);
        return positionIndex;
    }

    public void ResetGame()
    {
        foreach (Transform child in gridParent)
        {
            Destroy(child.gameObject);
        }
        SetupGrid();
    }
}