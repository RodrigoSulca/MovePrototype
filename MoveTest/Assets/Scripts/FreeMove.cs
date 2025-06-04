using UnityEngine;

public class FreeMove : MonoBehaviour
{
    public Camera cam; // Asigna la cámara principal o una específica
    public float zDistance = 10f; // Distancia desde la cámara al plano donde está el objeto
    public float minX = -10f; // Limite izquierdo
    public float maxX = 10f;  // Limite derecho

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = zDistance;

        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);

        // Solo cambiar la posición en X
        Vector3 newPos = transform.position;
        newPos.x = Mathf.Clamp(worldPos.x, minX, maxX); // Limita el movimiento
        transform.position = newPos;
    }
}
