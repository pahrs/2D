using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour

{
    public Transform target1; // Referência ao primeiro GameObject
    public Transform target2; // Referência ao segundo GameObject
    public float minZoom = 40f; // Zoom mínimo da câmera (campo de visão maior)
    public float maxZoom = 10f; // Zoom máximo da câmera (campo de visão menor)
    public float zoomLimiter = 50f; // Limitador do zoom

    private Camera cam; // Referência à câmera

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (target1 == null || target2 == null)
            return;

        MoveCamera();
        ZoomCamera();
    }

    void MoveCamera()
    {
        // Calcula o ponto médio entre os dois GameObjects
        Vector3 centerPoint = (target1.position + target2.position) / 2f;

    }

    void ZoomCamera()
    {
        // Calcula a distância entre os dois GameObjects
        float distance = Vector3.Distance(target1.position, target2.position);

        // Ajusta o campo de visão da câmera com base na distância
        float desiredZoom = Mathf.Lerp(maxZoom, minZoom, distance / zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, desiredZoom, Time.deltaTime);
    }
}
