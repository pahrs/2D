using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform[] targets; // Array de GameObjects (personagens)
    public Transform centerPoint; // Centro fixo da tela
    public float minDistance = 5f; // Distância mínima da câmera
    public float maxDistance = 20f; // Distância máxima da câmera
    public float zoomSpeed = 5f; // Velocidade de ajuste do zoom
    public float focusThreshold = 50f; // Distância máxima para que um personagem seja considerado no foco

    private Camera cam; // Referência à câmera
    private float targetOrthographicSize; // Tamanho ortográfico alvo da câmera

    private void Start()
    {
        cam = GetComponent<Camera>();
        targetOrthographicSize = cam.orthographicSize;
    }

    private void LateUpdate()
    {
        if (targets.Length == 0 || centerPoint == null)
            return;

        MoveCamera();
        ZoomCamera();
    }

    void MoveCamera()
    {
        // Calcula a média das posições dos personagens dentro do limite de foco
        Vector3 averagePosition = Vector3.zero;
        int numTargets = 0;

        foreach (Transform target in targets)
        {
            if (Vector3.Distance(target.position, centerPoint.position) <= focusThreshold)
            {
                averagePosition += target.position;
                numTargets++;
            }
        }

        if (numTargets > 0)
        {
            averagePosition /= numTargets;

            // Define a posição do centro da tela na média das posições dos personagens considerados
            centerPoint.position = averagePosition;
        }
    }

    void ZoomCamera()
    {
        // Calcula a distância máxima dos personagens em relação ao centro da tela dentro do limite de foco
        float maxDistanceToCenter = 0f;

        foreach (Transform target in targets)
        {
            float distance = Vector3.Distance(target.position, centerPoint.position);
            if (distance <= focusThreshold)
            {
                maxDistanceToCenter = Mathf.Max(maxDistanceToCenter, distance);
            }
        }

        // Calcula a distância normalizada entre minDistance e maxDistance
        float normalizedDistance = Mathf.Clamp01((maxDistanceToCenter - minDistance) / (maxDistance - minDistance));

        // Calcula o tamanho ortográfico alvo da câmera com base na distância normalizada
        float targetSize = Mathf.Lerp(minDistance, maxDistance, normalizedDistance);

        // Suaviza o ajuste do zoom da câmera
        targetOrthographicSize = Mathf.Lerp(targetOrthographicSize, targetSize, Time.deltaTime * zoomSpeed);

        // Define o tamanho ortográfico da câmera
        cam.orthographicSize = targetOrthographicSize;
    }
}
