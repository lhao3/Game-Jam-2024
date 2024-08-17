using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkableScript : MonoBehaviour
{
    public string size;
<<<<<<< Updated upstream
    public int xScale;
    public int yScale;
=======
    public float xScale = 1f; // Default value, adjust as needed
    public float yScale = 1f; // Default value, adjust as needed
    const float maxSize = 2f; // Max size
    const float minSize = 0.5f; // Min size
    [SerializeField] private float scaleSpeed = 1f; // Speed of scaling
    private Vector3 normalScale;

    private Vector3 targetScale;
    private bool scaling = false;
    private float scaleFactor;

>>>>>>> Stashed changes
    // Start is called before the first frame update
    void Start()
    {
        size = "normal";
<<<<<<< Updated upstream
=======
       normalScale = transform.localScale;
>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream
        
    }
=======
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
>>>>>>> Stashed changes

    void Shrink()
    {
        if (CheckSize())
        {
            size = "small";
        }
    }

    void Grow()
    {
        if (CheckSize())
        {
            size = "large";
        }
    }

    // TODO: come up with algorithm here
    // if resulting dimensions overlap with other objects/environemnt, return false, else return true
    private bool CheckSize()
    {
        return true;
    }
}
