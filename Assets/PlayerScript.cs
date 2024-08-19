using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float scaleSpeed = 1f;
    [SerializeField] private GameObject laser;
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] public float laserX;
    [SerializeField] public float laserY;
    [SerializeField] private float laserCooldownTime = 1.0f;

    public float xScale = 1f;
    public float yScale = 1f;
    [SerializeField] const float maxSize = 0.4f;  // Max size
    [SerializeField] const float minSize = 0.1f;
    private float scaleFactor;
    private bool scaling = false;
    private Vector3 targetScale;

    private Vector3 normalScale;

    [SerializeField] public float movementSpeed = 5f;
    [SerializeField] public float jumpForce = 2f;
    private string size;
    public float shrinkFactor = 0.5f;
    public float growFactor = 1.5f;
    private float horizontalMovement;
    private Rigidbody2D rb2D;
    private bool hasJumped = false;
    public Collider2D floorCollider;
    public ContactFilter2D floorFilter;
    private Vector3 laserPosition;
    private SpriteRenderer laserSprite;
    private Animator animator;
    public bool shrinkToggle = true;
    private float lastShootTime = -Mathf.Infinity;
    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        size = "normal";
        normalScale = transform.localScale;
        laserSprite = laser.GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.W) && isOnGround())
        {
            hasJumped = true;
            Debug.Log("Player has jumped");
        }

        if (animator != null)
        {
            if (horizontalMovement == 0)
            {
                animator.SetFloat("Speed", 0);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                playerSprite.flipX = true;
                animator.SetFloat("Speed", 0.5f);
                Debug.Log("Pressed A");
                Debug.Log("Grounded: " + isOnGround());
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                playerSprite.flipX = false;
                animator.SetFloat("Speed", 0.5f);
                Debug.Log("Pressed D");
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && size != "grown")
        {
            if (size.Equals("shrunk"))
            {
                size = "normal";
                SetScaling(normalScale, 1f);   // Grow back to normal size if shrunken
            }
            else
            {
                size = "grown";
                Vector3 grownScale = new Vector3(maxSize, maxSize, 1f);
                SetScaling(grownScale, 1.5f);   // Grow to max size if not shrunken
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && size != "shrunk")
        {
            if (size.Equals("grown"))
            {
                size = "normal";
                SetScaling(normalScale, 1f);   // Shrink back to normal size if grown
            }
            else
            {
                size = "shrunk";
                Vector3 shrunkenScale = new Vector3(minSize, minSize, 1f);
                SetScaling(shrunkenScale, 0.5f);    // Shrink to min size if not grown
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            shrinkToggle = true;
            Debug.Log($"Shrink Toggle is now: {shrinkToggle}");
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            shrinkToggle = false;
            Debug.Log($"Shrink Toggle is now: {shrinkToggle}");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time >= lastShootTime + laserCooldownTime)
            {
                ShootLaser();
                lastShootTime = Time.time;
            }
        }

        if (scaling)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, scaleSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.localScale, targetScale) < 0.01f) // Stops scaling
            {
                transform.localScale = targetScale;
                scaling = false;
            }
        }
    }

    public bool isOnGround()
    {
        return Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position - transform.up * castDistance, boxSize);
    }

    private void FixedUpdate()
    {
        rb2D.velocity = new Vector2(horizontalMovement * movementSpeed, rb2D.velocity.y);
        if (isOnGround() && hasJumped)
        {
            hasJumped = false;
            rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void ShootLaser()
    {
        bool isFlipped = playerSprite.flipX;

        if (isFlipped)
        {
            laserPosition = new Vector3(transform.position.x - laserX, transform.position.y + laserY, 0);
        }
        else
        {
            laserPosition = new Vector3(transform.position.x + laserX, transform.position.y + laserY, 0);
        }

        GameObject instantiatedLaser = Instantiate(laser, laserPosition, transform.rotation);
        SpriteRenderer laserSpriteRenderer = instantiatedLaser.GetComponent<SpriteRenderer>();
        instantiatedLaser.transform.SetParent(transform);

        if (playerSprite.flipX)
        {
            instantiatedLaser.transform.localScale = new Vector3(-Mathf.Abs(instantiatedLaser.transform.localScale.x), instantiatedLaser.transform.localScale.y, instantiatedLaser.transform.localScale.z);
            instantiatedLaser.transform.right = Vector3.left;
        }
        else
        {
            instantiatedLaser.transform.localScale = new Vector3(Mathf.Abs(instantiatedLaser.transform.localScale.x), instantiatedLaser.transform.localScale.y, instantiatedLaser.transform.localScale.z);
            instantiatedLaser.transform.right = Vector3.right;
        }
    }

      public void SetScaling(Vector3 targetSize, float factor)
{
    Vector3 originalScale = transform.localScale;

    if (factor > 1f)  // Check if growing
    {
        transform.localScale = targetSize;  // Temporarily apply the growth
        if (!CanGrow(targetSize))
        {
            Debug.Log("Cannot grow; space occupied or player is stuck.");
            transform.localScale = originalScale;  // Revert to original size if growth isn't possible
            return;
        }
    }

    targetScale = targetSize;
    scaling = true;
    scaleFactor = factor;
}


private bool CanGrow(Vector3 targetScale)
{
    // Calculate the future size of the player based on the target scale
    Vector2 futureSize = new Vector2(
        GetComponent<Collider2D>().bounds.size.x * (targetScale.x / transform.localScale.x),
        GetComponent<Collider2D>().bounds.size.y * (targetScale.y / transform.localScale.y)
    );

    Debug.Log($"Checking growth possibility. Current scale: {transform.localScale}, Target scale: {targetScale}, Future size: {futureSize}");

    // Perform the overlap check using the future size
    Collider2D[] colliders = Physics2D.OverlapBoxAll(
        transform.position, 
        futureSize, 
        0f, 
        LayerMask.GetMask("Ground")
    );

    if (colliders.Length == 0)
    {
        Debug.Log("Growth possible: No obstacles detected.");
        return true;
    }
    else
    {
        Debug.Log($"Growth not possible: Found {colliders.Length} obstacles.");
        return false;
    }
}

// Helper method to determine if the obstacle is in a position that would block growth
private bool IsBlockingGrowth(Collider2D collider)
{
    // Get bounds of the current player and the potential collider
    Bounds playerBounds = GetComponent<Collider2D>().bounds;
    Bounds obstacleBounds = collider.bounds;

    // Check if the obstacle is in close proximity in such a way that it would block growth
    bool isBlockingX = Mathf.Abs(playerBounds.center.x - obstacleBounds.center.x) < (playerBounds.extents.x + obstacleBounds.extents.x);
    bool isBlockingY = Mathf.Abs(playerBounds.center.y - obstacleBounds.center.y) < (playerBounds.extents.y + obstacleBounds.extents.y);

    return isBlockingX && isBlockingY;
}



private bool CheckStuck()
{
    // Define directions to check for stuck state
    Vector2[] directions = new Vector2[]
    {
        Vector2.up,
        Vector2.down,
        Vector2.left,
        Vector2.right
    };

    Debug.Log("Checking if player is stuck...");

    foreach (Vector2 direction in directions)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, boxSize.magnitude, LayerMask.GetMask("Default"));
        if (hit.collider != null)
        {
            Debug.Log($"Player is stuck in direction {direction}. Hit: {hit.collider.name}");
            return true;
        }
    }

    Debug.Log("Player is not stuck.");
    return false;
}



    public bool GetShrinkToggle()
    {
        return shrinkToggle;
    }

    public void HitWeb()
    {
        movementSpeed -= 4f;
        print("Player successfully slowed");
    }

    public void ExitWeb()
    {
        movementSpeed += 4f;
        print("Player successfully exited web... increasing speed back to normal.");
    }
}
