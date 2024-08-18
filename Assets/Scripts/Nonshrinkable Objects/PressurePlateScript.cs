using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateScript : NonShrinkableScript
{

    public bool activated;
    DoorScript linkedDoor;

    // Start is called before the first frame update
    void Start()
    {
        activated = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            linkedDoor.opened = true;
        }

    }
}
