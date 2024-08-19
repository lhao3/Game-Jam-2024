using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateScript : NonShrinkableScript
{

    public bool activated;
    public GameObject linkedObject;
    public Sprite unpressedPressurePlate;
    public Sprite pressedPressurePlate;

    // Start is called before the first frame update
    void Start()
    {
        activated = false;
        

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        activated = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = pressedPressurePlate;

        if (linkedObject.name == "Growing Vine")
        {
            linkedObject.SendMessage("Activate");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        activated = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = unpressedPressurePlate;
    }
}
