using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkableScript : MonoBehaviour
{
    public string size;

    public int xScale;
    public int yScale;

    public float xScale = 1f; // Default value, adjust as needed
    public float yScale = 1f; // Default value, adjust as needed
    const float maxSize = 2f; // Max size
    const float minSize = 0.5f; // Min size
    [SerializeField] private float scaleSpeed = 1f; // Speed of scaling
    private Vector3 normalScale;

    private Vector3 targetScale;
    private bool scaling = false;
    private float scaleFactor;


    // Start is called before the first frame update
    void Start()
    {
        size = "normal";

        normalScale = transform.localScale;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            if (size == "normal" || size == "large")
            {
                SetScaling(minSize, 0.5f); // Shrink to minSize
                size = "small";
            }
            else if (size == "small")
            {
                SetScaling(maxSize, 1.5f); // Grow to maxSize
                size = "large";
            }
        }*/


    void Shrink()
    {
        if (CheckSize())
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (size == "normal" || size == "large")
            {
                SetScaling(minSize, 0.5f); // Shrink to minSize
                size = "small";
            }
            else if (size == "small")
            {
                SetScaling(maxSize, 1.5f); // Grow to maxSize
                size = "large";
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
    }

    private void SetScaling(float targetSize, float factor)
    {
        targetScale = new Vector3(targetSize, targetSize, 1f);
        scaling = true;
        scaleFactor = factor;
    }
}
