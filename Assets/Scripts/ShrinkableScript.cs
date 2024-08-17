using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkableScript : MonoBehaviour
{
    public string size;
    // Start is called before the first frame update
    void Start()
    {
        size = "normal";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Shrink()
    {
        size = "small";
    }

    void Grow()
    {
        size = "large";
    }
}
