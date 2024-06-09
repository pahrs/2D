using UnityEngine;

public class CameraMoveUp : MonoBehaviour
{
    public float initialSpeed = 0.1f;  
    public float speedIncreaseInterval = 40f;  
    public float speedMultiplier = 1.2f;  

    private float currentSpeed;
    private float timeElapsed;

    void Start()
    {
        currentSpeed = initialSpeed;
        timeElapsed = 0f;
    }

    void Update()
    {
        transform.Translate(Vector3.up * currentSpeed * Time.deltaTime);

        timeElapsed += Time.deltaTime;

        if (timeElapsed >= speedIncreaseInterval)
        {
            currentSpeed *= speedMultiplier;
            timeElapsed = 0f; 
        }
    }
}