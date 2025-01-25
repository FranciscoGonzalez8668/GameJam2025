using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    private Vector2 bottomLeft;
    private Vector2 topLeft;
    private Vector2 topRight;
    private Vector2 bottomRight;

    void Start()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            bottomLeft = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
            topLeft = mainCamera.ViewportToWorldPoint(new Vector2(0, 1));
            topRight = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
            bottomRight = mainCamera.ViewportToWorldPoint(new Vector2(1, 0));

            CreateEdgeColliders(mainCamera);
        }
    }

    void CreateEdgeColliders(Camera camera)
    {
        GameObject edgeColliders = new GameObject("EdgeColliders");
        edgeColliders.transform.parent = camera.transform;

        // Crear colisionador para el borde inferior
        EdgeCollider2D bottomCollider = edgeColliders.AddComponent<EdgeCollider2D>();
        Vector2[] bottomPoints = new Vector2[2];
        bottomPoints[0] = bottomLeft;
        bottomPoints[1] = bottomRight;
        bottomCollider.points = bottomPoints;
        bottomCollider.tag = "BottomEdge"; // Asignar etiqueta para identificar el borde inferior

        // Crear colisionador para el borde lateral izquierdo (tercio inferior)
        EdgeCollider2D leftCollider = edgeColliders.AddComponent<EdgeCollider2D>();
        Vector2[] leftPoints = new Vector2[2];
        leftPoints[0] = bottomLeft;
        leftPoints[1] = new Vector2(bottomLeft.x, bottomLeft.y + (topLeft.y - bottomLeft.y) / 3);
        leftCollider.points = leftPoints;

        // Crear colisionador para el borde lateral derecho (tercio inferior)
        EdgeCollider2D rightCollider = edgeColliders.AddComponent<EdgeCollider2D>();
        Vector2[] rightPoints = new Vector2[2];
        rightPoints[0] = bottomRight;
        rightPoints[1] = new Vector2(bottomRight.x, bottomRight.y + (topRight.y - bottomRight.y) / 3);
        rightCollider.points = rightPoints;
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if (Camera.main != null)
        {
            // Obtener las esquinas de la cámara en coordenadas del mundo
            bottomLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
            topLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 1));
            topRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
            bottomRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 0));

            // Dibujar los bordes de la cámara
            Gizmos.color = Color.red;
            // Borde inferior
            Gizmos.DrawLine(bottomLeft, bottomRight);
            // Borde lateral izquierdo (tercio inferior)
            Gizmos.DrawLine(bottomLeft, new Vector2(bottomLeft.x, bottomLeft.y + (topLeft.y - bottomLeft.y) / 3));
            // Borde lateral derecho (tercio inferior)
            Gizmos.DrawLine(bottomRight, new Vector2(bottomRight.x, bottomRight.y + (topRight.y - bottomRight.y) / 3));
        }
#endif
    }
}