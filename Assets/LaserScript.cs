using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    public LayerMask layersToHit;

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
        if(hit.collider == null)
        {
            transform.localScale = new Vector3(5f, transform.localScale.y, 1);
            return;
        }

        transform.localScale = new Vector3(hit.distance, transform.localScale.y, 1);
        Debug.Log(hit.collider.gameObject.name);

        if(hit.collider.tag == "Player")
        {
            //add shrink/grow mechanic here 
            Destroy(hit.collider.gameObject);
        }
    }
}
