using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformsFalling : MonoBehaviour
{
    public float fallSpeed = 1.5f;
    public double multiplierFallSpeed = 1.001;
    private Rigidbody2D rb; 

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        StartCoroutine(IncreaseFallSpeedOverTime());
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

        IEnumerator IncreaseFallSpeedOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            fallSpeed *= (float)multiplierFallSpeed; // Aumenta a velocidade de queda em 0,1%
        }
    }
}

    