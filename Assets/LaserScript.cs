using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    public LayerMask layersToHit;
    private Collider2D collided; 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float angle = transform.eulerAngles.z * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 50f, layersToHit);
        if (hit.collider == null)
        {
            transform.localScale = new Vector3(1f, transform.localScale.y, 1);
            return;
        }

        transform.localScale = new Vector3(hit.distance, transform.localScale.y, 1);
        Debug.Log(hit.collider.gameObject.name);

        if (hit.collider.tag == "Shrinkable")
        {
            collided = hit.collider; 
            TriggerShrink();
            /*StartCoroutine(WaitBeforeShrink());

            ShrinkableScript shrinkable = hit.collider.GetComponent<ShrinkableScript>();

            if (shrinkable != null)
            {
                Debug.Log("shrinking");
                shrinkable.Shrink();
            }*/
        }

        
    }

    public void TriggerShrink()
    {
        StartCoroutine(WaitBeforeShrink());
    }

    private IEnumerator WaitBeforeShrink()
    {
        yield return new WaitForSeconds(0.51f);

        ShrinkableScript shrinkable = collided.GetComponent<ShrinkableScript>();

        if (shrinkable != null)
        {
            Debug.Log("shrinking");
            shrinkable.Shrink();
        }
    }

}
