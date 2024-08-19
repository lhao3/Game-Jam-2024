using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCheckScript : MonoBehaviour
{
    public LayerMask collisionMask;
    private Vector2 originalSize;
    public float normalX = 0.96f;
    public float normalY = 1.15f;
    [SerializeField] private float yOffset;
    [SerializeField] private float yOffsetsmall;
    private Vector2 boxSize;
    private Vector3 offsetPosition;
    // Start is called before the first frame update
    void Start()
    {
        originalSize = GetComponent<Collider2D>().bounds.size;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool CheckSpace(bool normalSize)
    {
        // Calculate the size for the OverlapBox based on the player's desired scale

        if (normalSize)
        {
            boxSize = new Vector2(2 * normalX, 2 * normalY);
            offsetPosition = new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z);
        }
        else
        {
            boxSize = new Vector2(normalX, normalY);
            offsetPosition = new Vector3(transform.position.x, transform.position.y + yOffsetsmall, transform.position.z);
        }

        // Apply the offset to the position where the OverlapBox will be checked

        // Perform the OverlapBox check
        Collider2D[] hits = Physics2D.OverlapBoxAll(offsetPosition, boxSize, 0f, collisionMask);

        // Visualize the box using Debug.DrawLine (creating a visual outline)
        Vector3 bottomLeft = offsetPosition - new Vector3(boxSize.x / 2, boxSize.y / 2, 0);
        Vector3 bottomRight = offsetPosition + new Vector3(boxSize.x / 2, -boxSize.y / 2, 0);
        Vector3 topLeft = offsetPosition + new Vector3(-boxSize.x / 2, boxSize.y / 2, 0);
        Vector3 topRight = offsetPosition + new Vector3(boxSize.x / 2, boxSize.y / 2, 0);

        Debug.DrawLine(bottomLeft, bottomRight, Color.green, 10f);
        Debug.DrawLine(bottomLeft, topLeft, Color.green, 10f);
        Debug.DrawLine(topLeft, topRight, Color.green, 10f);
        Debug.DrawLine(topRight, bottomRight, Color.green, 10f);

        // Log the size and position for debugging
        Debug.Log($"Checking space with box size: {boxSize}, at offset position: {offsetPosition}");
        Debug.Log($"Number of hits detected: {hits.Length}");

        // Determine if the space is empty (no colliders in the area)
        bool isEmpty = hits.Length == 0;

        // Log the result of the check
        Debug.Log($"Is the space empty? {isEmpty}");

        return isEmpty;
    }

    // This function is called to draw Gizmos, which are always visible in the scene view
   /* private void OnDrawGizmos()
    {
        if (originalSize == Vector2.zero)
            return; // Avoid drawing if originalSize isn't set

        // Use a default scale for visualization in editor mode
        float scale = 1f;
        Vector2 boxSize = new Vector2(originalSize.x * scale, originalSize.y * scale);

        Vector3 bottomLeft = transform.position - new Vector3(boxSize.x / 2, boxSize.y / 2, 0);
        Vector3 bottomRight = transform.position + new Vector3(boxSize.x / 2, -boxSize.y / 2, 0);
        Vector3 topLeft = transform.position + new Vector3(-boxSize.x / 2, boxSize.y / 2, 0);
        Vector3 topRight = transform.position + new Vector3(boxSize.x / 2, boxSize.y / 2, 0);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(bottomLeft, topLeft);
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
    }*/

}
