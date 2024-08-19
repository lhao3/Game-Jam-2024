using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float scaleSpeed = 1f;
    [SerializeField] private GameObject laser;
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] public float laserX;
    [SerializeField] public float laserY;
    [SerializeField] private float laserCooldownTime = 1.0f;
    [SerializeField] private GameObject boxCheck;


    public float xScale = 1f;
    public float yScale = 1f;
    [SerializeField] const float maxSize = 0.4f;  //Max size
    [SerializeField] const float minSize = 0.1f;
    private float scaleFactor;
    private bool scaling = false;
    private Vector3 targetScale;

    private Vector3 normalScale;

    [SerializeField] public float movementSpeed = 5f;
    [SerializeField] public float jumpForce = 2f;
    private string size;
    public float shrinkFactor = 0.5f;
    public float growFactor = 2f;
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
    public float yOffsetSmall;
    public float yOffsetLarge;
    public float castDistance;
    public LayerMask groundLayer;
    public Vector3 pos;
 




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

            if (!CollisionCheck())
            {
                return;
            }

            if (size.Equals("shrunk"))
            {  
                size = "normal";
                SetScaling(normalScale, 1f);   //grow back to normal size if shrunken
            }
            else
            {
                size = "grown";
                Vector3 grownScale = new Vector3(maxSize, maxSize, 1f);
                SetScaling(grownScale, 1.5f);   //grow to max size if not shrunken
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && size != "shrunk")
        {

            if (size.Equals("grown"))
            {
                size = "normal";
                SetScaling(normalScale, 1f);   //shrink back to normal size if grown
            }
            else
            {
                size = "shrunk";
                Vector3 shrunkenScale = new Vector3(minSize, minSize, 1f);
                SetScaling(shrunkenScale, 0.5f);    //shrink to min size if not grown
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
            if (Vector3.Distance(transform.localScale, targetScale) < 0.01f) //stops scaling
            {
                transform.localScale = targetScale;
                scaling = false;
            }

        }

    }

    public bool isOnGround()
    {

        if(size == "shrunk")
        {
            pos = new Vector3(transform.position.x, transform.position.y + yOffsetSmall, transform.position.z);
        }
        else if(size == "normal")
        {
            pos = transform.position;
        }
        else
        {
            pos = new Vector3(transform.position.x, transform.position.y + yOffsetLarge, transform.position.z);
        }

        if(Physics2D.BoxCast(pos, boxSize, 0, -transform.up, castDistance, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Vector3 center = transform.position - (transform.up * castDistance / 2);
        //Gizmos.DrawWireCube(center, boxSize);
        Gizmos.DrawCube(pos - transform.up * castDistance, boxSize);


    }

    public bool CollisionCheck()
    {
        GameObject temp = Instantiate(boxCheck, transform.position, Quaternion.identity);

        BoxCheckScript boxCheckScript = temp.GetComponent<BoxCheckScript>();
        bool checkNormalSize;

        if(size == "normal"){
            checkNormalSize = true;
        }
        else
        {
            checkNormalSize = false;
        }

        bool isRoom = boxCheckScript.CheckSpace(checkNormalSize);

        Destroy(temp);

        return isRoom;
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

        targetScale = targetSize;
        scaling = true;
        scaleFactor = factor;
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
