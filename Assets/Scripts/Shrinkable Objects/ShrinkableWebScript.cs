using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkableWebScript : MonoBehaviour
{


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
    private float scaleFactor;
    private string size;


    // Start is called before the first frame update
    void Start()
    {
        size = "normal";
       
        normalScale = transform.localScale;

    }

    // Update is called once per frame
    void Update()
    {
       /* if (Input.GetKeyDown(KeyCode.P) && size != "large")
        {

            if (size.Equals("small"))
            {
                size = "normal";
                SetScaling(normalScale, 1f);   //grow back to normal size if shrunken
            }
            else
            {
                size = "large";
                Vector3 grownScale = new Vector3(maxXSize, maxYSize, 1f);
                SetScaling(grownScale, 1.5f);   //grow to max size if not shrunken
            }
        }

        if (Input.GetKeyDown(KeyCode.I) && size != "small")
        {

            if (size.Equals("large"))
            {
                size = "normal";
                SetScaling(normalScale, 1f);   //shrink back to normal size if grown
            }
            else
            {
                size = "small";
                Vector3 shrunkenScale = new Vector3(minXSize, minYSize, 1f);
                SetScaling(shrunkenScale, 0.5f);    //shrink to min size if not grown
            }
        }*/

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
    }

    public void Shrink()
    {
        if(size != "small")
        {
            if (size.Equals("large"))
            {
                size = "normal";
                SetScaling(normalScale, 1f);   //shrink back to normal size if grown
            }
            else
            {
                size = "small";
                Vector3 shrunkenScale = new Vector3(minXSize, minYSize, 1f);
                SetScaling(shrunkenScale, 0.5f);    //shrink to min size if not grown
            }
        }
    }

    public void Grow()
    {
        if(size != "large")
        {
            if (size.Equals("small"))
            {
                size = "normal";
                SetScaling(normalScale, 1f);   //grow back to normal size if shrunken
            }
            else
            {
                size = "large";
                Vector3 grownScale = new Vector3(maxXSize, maxYSize, 1f);
                SetScaling(grownScale, 1.5f);   //grow to max size if not shrunken
            }
        }
    }

    private void SetScaling(Vector3 targetsize, float factor)
    {
        targetScale = targetsize;
        scaling = true;
        scaleFactor = factor;
    }

    // TODO: come up with algorithm here
    // if resulting dimensions overlap with other objects/environemnt, return false, else return true
    private bool CheckSize()
    {
        return true;
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.SendMessage("HitWeb");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        other.gameObject.SendMessage("ExitWeb");
    }
}
