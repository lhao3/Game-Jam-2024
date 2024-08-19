using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    [SerializeField] private float laserLength;
    private PlayerScript playerScript;
    public LayerMask layersToHit;
    private Collider2D collided;
    private SpriteRenderer laserSprite;

    // Start is called before the first frame update
    void Start()
    {
        laserSprite = GetComponent<SpriteRenderer>();

        if (laserSprite == null)
        {
            Debug.LogError("SpriteRenderer component not found on Laser.");
            return;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerScript = player.GetComponent<PlayerScript>();
        }
        else
        {
            Debug.LogError("Player GameObject not found in the scene.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = transform.right;
        laserLength =  laserSprite.bounds.size.x;
        Debug.DrawRay(transform.position, dir * laserLength, Color.red);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, laserLength, layersToHit);
        if (hit.collider == null)
        {
            transform.localScale = new Vector3(3f, transform.localScale.y, 1);
            return;
        }
        else
        {
            float hitDistance = hit.distance;
            transform.localScale = new Vector3(hitDistance, transform.localScale.y, transform.localScale.z);
        }

        if (hit.collider.tag == "Shrinkable")
        {
            collided = hit.collider;
            TriggerScaling();
    
        }

        if(hit.collider != null)
        {
            Debug.Log($"Laser hit {hit.collider.name} at distance {hit.distance}");

        }
        else
        {
            Debug.Log("Laser did not hit anything");

        }



    }

    void SetLaserLength(float length)
    {
        Vector3 newScale = transform.localScale;
        newScale.x = length;
        transform.localScale = newScale;
    }




    public void TriggerScaling()
    {
        StartCoroutine(WaitBeforeShrink());
    }

    private IEnumerator WaitBeforeShrink()
    {
        yield return new WaitForSeconds(0.2f);
        //Destroy(gameObject);
        if (collided != null){
            ShrinkableScript shrinkable = collided.GetComponent<ShrinkableScript>();

            if (shrinkable != null)
            {
                bool shrinkToggleState = playerScript.GetShrinkToggle();
                Debug.Log($"Shrink Toggle at time of scaling: {shrinkToggleState}");

                if (playerScript.GetShrinkToggle())
                {
                    Debug.Log("shrinking");
                    shrinkable.Shrink();
                }
                else
                {
                    Debug.Log("growing");
                    shrinkable.Grow();
                }
                Debug.Log("" + playerScript.GetShrinkToggle());

            }
        }
    }

    private IEnumerator DelayScalingStart()
    {
        yield return new WaitForSeconds(0.1f); 
    }

    public void DestroyLaserEvent()
    {
        Destroy(gameObject);
    }


}
