using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateScript : NonShrinkableScript
{

    public bool activated;
    DoorScript linkedDoor;
    private SpriteRenderer sr;
    public Sprite unpressedPressurePlate;
    public Sprite pressedPressurePlate;

    // Start is called before the first frame update
    void Start()
    {
        activated = false;
        sr = gameObject.GetComponent<SpriteRenderer>();
        

    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            linkedDoor.opened = true;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = pressedPressurePlate;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = unpressedPressurePlate;
    }
}
