using System.Collections.Generic;
using UnityEngine;

public class DealerMarkerManager : MonoBehaviour
{
    public GameObject dealerMarkPrefab; // Prefab of the dealer marker
    public Transform[] dealerPositions; // Array to store dealer positions for each player

    private GameObject currentDealerMark; // Instance of the dealer mark prefab

    void Start()
    {
        // Check if the dealer mark prefab is assigned
        if (dealerMarkPrefab == null)
        {
            Debug.LogError("Dealer mark prefab is not assigned.");
        }

        // Validate dealer positions array
        if (dealerPositions == null || dealerPositions.Length < 6)
        {
            Debug.LogError($"Dealer positions array is either not assigned or incomplete. Assigned positions: {dealerPositions?.Length ?? 0}");
        }
        else
        {
            for (int i = 0; i < dealerPositions.Length; i++)
            {
                if (dealerPositions[i] == null)
                {
                    Debug.LogError($"Dealer position {i} is not assigned.");
                }
            }
        }
    }

    // Function to set the dealer mark position
    public void SetDealerMark(int dealerIndex)
    {
        // Check if dealer positions are correctly assigned
        if (dealerMarkPrefab == null || dealerPositions == null || dealerPositions.Length == 0)
        {
            Debug.LogError("Dealer mark or dealer positions are not set correctly.");
            return;
        }

        // Validate the dealer index
        if (dealerIndex >= 0 && dealerIndex < dealerPositions.Length)
        {
            // If the current dealer mark exists, destroy it before creating a new one
            if (currentDealerMark != null)
            {
                Destroy(currentDealerMark);
            }

            // Instantiate a new dealer mark and set it as a child of the correct player position
            currentDealerMark = Instantiate(dealerMarkPrefab, dealerPositions[dealerIndex]);
            currentDealerMark.transform.localPosition = Vector3.zero;
            currentDealerMark.SetActive(true);
        }
        else
        {
            Debug.LogError($"Invalid dealer index: {dealerIndex}. Dealer positions array length: {dealerPositions.Length}.");
        }
    }
}
