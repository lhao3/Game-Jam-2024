using UnityEngine;

public class ButtonInteraction : MonoBehaviour
{
    public Transform pillar; // Assign the pillar GameObject in the Inspector
    public float moveSpeed = 2f; // Speed at which the pillar moves up

    private bool isPlayerNear = false;
    private bool hasMoved = false; // Flag to check if the pillar has been moved

    private Vector3 targetPosition; // Target position for the pillar

    void Start()
    {
        if (pillar != null)
        {
            // Initialize the target position
            targetPosition = pillar.position + new Vector3(0, 5, 0); // Move up 5 units
        }
        else
        {
            Debug.LogError("Pillar is not assigned in the Inspector.");
        }
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F) && !hasMoved)
        {
            hasMoved = true; // Set the flag to true to prevent further movement
        }

        // Smoothly move the pillar if it has been activated
        if (hasMoved && pillar != null)
        {
            pillar.position = Vector3.MoveTowards(pillar.position, targetPosition, moveSpeed * Time.deltaTime);

            // Check if the pillar has reached the target position
            if (Vector3.Distance(pillar.position, targetPosition) < 0.01f)
            {
                pillar.position = targetPosition; // Ensure it reaches exactly the target position
                hasMoved = false; // Optionally reset the flag if you want to allow future interactions
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}
