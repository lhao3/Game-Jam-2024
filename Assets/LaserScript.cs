using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    [SerializeField] private float laserLength;
    [SerializeField] public PlayerScript playerScript;
    public LayerMask layersToHit;
    private Collider2D collided;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(DelayScalingStart());
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
            Debug.Log("shrinking");
            if (playerScript.GetShrinkToggle())
            {
                shrinkable.Shrink();
            }
            else
            {
                shrinkable.Grow();
            }
            
        }
    }

    private IEnumerator DelayScalingStart()
    {
        yield return new WaitForSeconds(0.1f); 
    }


}
