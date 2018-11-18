using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSprite : MonoBehaviour {

    public Sprite frontLeft;
    public Sprite frontRight;
    public Sprite backLeft;
    public Sprite backRight;

    private SpriteRenderer spriteRenderer;

    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
    }

    public void SetRotation(float rotation)
    {
        Debug.Log(rotation);
        if (rotation < 45.0f) {
            spriteRenderer.sprite = backLeft;
        }
        else if (rotation < 135.0f) {
            spriteRenderer.sprite = backRight;
        }
        else if (rotation < 225.0f) {
            spriteRenderer.sprite = frontRight;
        }
        else if (rotation < 315.0f) {
            spriteRenderer.sprite = frontLeft;
        }
        else {
            spriteRenderer.sprite = backLeft;
        }
    }

}
