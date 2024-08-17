using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkableScript : MonoBehaviour
{
    public string size;
    public int xScale;
    public int yScale;
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
