using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingVine : MonoBehaviour
{
    public bool activated;
    public Sprite VineStage1;
    public Sprite VineStage2;
    public Sprite VineStage3;
    public Sprite VineStage4;
    public float targetTime = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        activated = false;
    }

    // Update is called once per frame
    void Update()
    {
        targetTime -= Time.deltaTime;

        if (targetTime <= 0.0f)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = VineStage4;
        } else if (targetTime <= 5.0f)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = VineStage3;
        } else if (targetTime <= 10.0f)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = VineStage2;
        } 
    }

    void Activate()
    {
        activated = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = VineStage1;
    }
}
