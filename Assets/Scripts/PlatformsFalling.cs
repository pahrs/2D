using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformsFalling : MonoBehaviour
{
    private float fallSpeed = 1.5f;
    private Rigidbody2D rb; 

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    void Update()
    {
        
        rb.velocity = new Vector2(0, fallSpeed * -1);

        if (!IsVisible())
        {
            Destroy(gameObject);
        }
    }

    bool IsVisible()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        return viewportPosition.y > 0;
    }
}

    