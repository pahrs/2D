using UnityEngine;

public class CameraMoveUp : MonoBehaviour
{
    public float initialSpeed = 0.1f;
    public float speedIncreaseInterval = 40f;
    public float speedMultiplier = 1.2f;

    public bool gameEnd = false;

    private float currentSpeed;
    private float timeElapsed;
    private float totalElapsedTime;

    void Start()
    {
        currentSpeed = initialSpeed;
        timeElapsed = 0f;
        totalElapsedTime = 0f;
    }

    void Update()
    {
        if (totalElapsedTime >= 120f)
        {
            gameEnd = true;
            currentSpeed = 0f; 
            return; 
        }

        transform.Translate(Vector3.up * currentSpeed * Time.deltaTime);

        timeElapsed += Time.deltaTime;
        totalElapsedTime += Time.deltaTime;

        if (timeElapsed >= speedIncreaseInterval)
        {
            currentSpeed *= speedMultiplier;
            timeElapsed = 0f;
        }
    }
}