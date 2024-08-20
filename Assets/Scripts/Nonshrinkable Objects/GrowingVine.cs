using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class GrowingVine : MonoBehaviour
{
    public bool activated;
    public Sprite VineStage1;
    public Sprite VineStage2;
    public Sprite VineStage3;
    public Sprite VineStage4;
    public float targetTime;
    public BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float firstSizeX;
    [SerializeField] private float firstSizeY;
    [SerializeField] private float secondSizeY;
    [SerializeField] private float thirdSizeY;
    [SerializeField] private float fourthSizeY;
    [SerializeField] private float offsetX;
    [SerializeField] private float offsetY;

    // Start is called before the first frame update
    void Start()
    {
        activated = false;

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component is missing.");
        }

        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        if (boxCollider == null)
        {
            Debug.LogError("BoxCollider2D component is missing.");
        }
        Vector2 size = new Vector2(firstSizeX, fourthSizeY);
        Vector2 offset = new Vector2(offsetX, offsetY);
        UpdateColliderSize(size, offset);
    }

    // Update is called once per frame
    void Update()
    {
        if (!activated)
            return;

        targetTime -= Time.deltaTime;

        if (targetTime <= 0.0f)
        {
            spriteRenderer.sprite = VineStage4;
            Vector2 size = new Vector2(firstSizeX, fourthSizeY);
            Vector2 offset = new Vector2(offsetX, offsetY);
            UpdateColliderSize(size, offset);
            activated = false;
        }
        else if (targetTime <= 5.0f)
        {
            spriteRenderer.sprite = VineStage3;
            Vector2 size = new Vector2(firstSizeX, thirdSizeY);
            Vector2 offset = new Vector2(offsetX, offsetY);
            UpdateColliderSize(size, offset);
        }
        else if (targetTime <= 10.0f)
        {
            spriteRenderer.sprite = VineStage2;
            Vector2 size = new Vector2(firstSizeX, secondSizeY);
            Vector2 offset = new Vector2(offsetX, offsetY);
            UpdateColliderSize(size, offset);
        }

    }

    void Activate()
    {
        print("growing vine activated");
        targetTime = 15.0f;
        activated = true;
        spriteRenderer.sprite = VineStage1;
        Vector2 size = new Vector2(firstSizeX, firstSizeY);
        Vector2 offset = new Vector2(offsetX, offsetY);
        UpdateColliderSize(size, offset);
    }
    private void UpdateColliderSize(Vector2 manualSize, Vector2 manualOffset)
    {
        if (boxCollider != null)
        {
            boxCollider.size = manualSize;
            boxCollider.offset = manualOffset;




            /*Bounds bounds = boxCollider.bounds;
            Vector3 min = bounds.min;
            Vector3 max = bounds.max;

            Debug.DrawLine(new Vector3(min.x, min.y, 0), new Vector3(max.x, min.y, 0), Color.blue, 10f);
            Debug.DrawLine(new Vector3(max.x, min.y, 0), new Vector3(max.x, max.y, 0), Color.blue, 10f);
            Debug.DrawLine(new Vector3(max.x, max.y, 0), new Vector3(min.x, max.y, 0), Color.blue, 10f);
            Debug.DrawLine(new Vector3(min.x, max.y, 0), new Vector3(min.x, min.y, 0), Color.blue, 10f);*/
        }
        else
        {
            Debug.LogError("BoxCollider2D is missing.");
        }
    }

   
}
