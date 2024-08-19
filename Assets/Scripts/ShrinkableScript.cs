using UnityEngine;

public class ShrinkableScript : MonoBehaviour
{
    public string size;

    [SerializeField]
    public float maxXSize = 2f; // Max X size

    [SerializeField]
    public float maxYSize = 2f; // Max Y size

    [SerializeField]
    public float minXSize = 0.5f; // Min X size

    [SerializeField]
    public float minYSize = 0.5f; // Min Y size

    [SerializeField] private float scaleSpeed = 1f; // Speed of scaling
    private Vector3 normalScale;
    private Vector3 targetScale;
    private bool scaling = false;
    private Collider2D objectCollider2D;
    private bool isColliding = false;

    void Start()
    {
        size = "normal";
        objectCollider2D = GetComponent<Collider2D>();
        normalScale = transform.localScale;
    }

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.P) && size != "large")
        {
            if (size.Equals("small"))
            {
                size = "normal";
                SetScaling(normalScale); // Grow back to normal size if shrunken
            }
            else
            {
                Vector3 grownScale = new Vector3(maxXSize, maxYSize, 1f);
                if (CheckSize(grownScale))
                {
                    size = "large";
                    SetScaling(grownScale); // Grow to max size if not shrunken
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.I) && size != "small")
        {
            if (size.Equals("large"))
            {
                size = "normal";
                SetScaling(normalScale); // Shrink back to normal size if grown
            }
            else
            {
                Vector3 shrunkenScale = new Vector3(minXSize, minYSize, 1f);
                SetScaling(shrunkenScale); // Shrink to min size if not grown
            }
        }

        if (scaling)
        {
            // Gradually scale towards the target size
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, scaleSpeed * Time.deltaTime);

            // Stop scaling if close enough to the target scale
            if (Vector3.Distance(transform.localScale, targetScale) < 0.01f)
            {
                transform.localScale = targetScale;
                scaling = false;
            }
        }
        */
    }

    public void Shrink()
    {
        if (size != "small")
        {
            if (size.Equals("large"))
            {
                size = "normal";
                SetScaling(normalScale); // Shrink back to normal size if grown
            }
            else
            {
                Vector3 shrunkenScale = new Vector3(minXSize, minYSize, 1f);
                SetScaling(shrunkenScale); // Shrink to min size if not grown
            }
        }
    }

    public void Grow()
    {
        if (size != "large")
        {
            if (size.Equals("small"))
            {
                size = "normal";
                SetScaling(normalScale); // Grow back to normal size if shrunken
            }
            else
            {
                Vector3 grownScale = new Vector3(maxXSize, maxYSize, 1f);
                if (CheckSize(grownScale))
                {
                    size = "large";
                    SetScaling(grownScale); // Grow to max size if not shrunken
                }
            }
        }
    }

    private void SetScaling(Vector3 targetSize)
    {
        targetScale = targetSize;
        scaling = true;
        Debug.Log($"Setting scaling to target size: {targetSize}");
    }

    private bool CheckSize(Vector3 newScale)
    {
        // Temporarily apply the new scale
        Vector3 tempScale = transform.localScale;
        transform.localScale = newScale;

        // Define the bounds of the object in its new size
        Bounds bounds = new Bounds(transform.position, transform.localScale);
        Debug.Log($"Checking collisions with bounds: Center = {bounds.center}, Size = {bounds.size}");

        // Check for overlaps with colliders in the area where the object will scale
        Collider2D[] colliders = Physics2D.OverlapBoxAll((Vector2)bounds.center, (Vector2)bounds.size, 0f);

        isColliding = false;
        foreach (Collider2D collider in colliders)
        {
            // Ignore collisions with self
            if (collider != objectCollider2D)
            {
                // Check if the collision is blocking
                if (IsBlockingCollision(collider, bounds))
                {
                    isColliding = true;
                    Debug.Log($"Collision detected with {collider.gameObject.name}");
                    transform.localScale = tempScale;
                    return false;
                }
            }
        }

        // Restore the original scale
        transform.localScale = tempScale;
        if (!isColliding)
        {
            Debug.Log("No collisions detected. Scaling is allowed.");
        }
        return !isColliding;
    }

    private bool IsBlockingCollision(Collider2D collider, Bounds bounds)
    {
        // Calculate the bounds of the other collider
        Bounds otherBounds = collider.bounds;

        // Ensure there is space for the object to fit
        return !bounds.Intersects(otherBounds);
    }
}
