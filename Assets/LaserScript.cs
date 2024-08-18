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

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(DelayScalingStart());
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
        Debug.DrawRay(transform.position, dir * laserLength, Color.red);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, laserLength, layersToHit);
        if (hit.collider == null)
        {
            transform.localScale = new Vector3(1f, transform.localScale.y, 1);
            return;
        }

        transform.localScale = new Vector3(hit.distance, transform.localScale.y, 1);
        //Debug.Log(hit.collider.gameObject.name);

        if (hit.collider.tag == "Shrinkable")
        {
            collided = hit.collider;
            TriggerScaling();
    
        }



    }

    public void TriggerScaling()
    {
        StartCoroutine(WaitBeforeShrink());
    }

    private IEnumerator WaitBeforeShrink()
    {
        yield return new WaitForSeconds(0.51f);
        Destroy(gameObject);
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

    private IEnumerator DelayScalingStart()
    {
        yield return new WaitForSeconds(0.1f); 
    }


}
