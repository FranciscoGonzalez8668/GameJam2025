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

        // Crear colisionador para el borde lateral izquierdo (mitad inferior)
        EdgeCollider2D leftColliderHalf = edgeColliders.AddComponent<EdgeCollider2D>();
        Vector2[] leftPoints = new Vector2[2];
        leftPoints[0] = bottomLeft;
        leftPoints[1] = new Vector2(bottomLeft.x, bottomLeft.y + (topLeft.y - bottomLeft.y) / 4*2);
        leftColliderHalf.points = leftPoints;

        //Crear Colisionador para el borde lateral izquierdo (cuarto superior)
        EdgeCollider2D leftColliderQuarter = edgeColliders.AddComponent<EdgeCollider2D>();
        Vector2[] leftPointQuarter = new Vector2[2];
        leftPointQuarter[0]= new Vector2(bottomLeft.x, bottomLeft.y + (topLeft.y - bottomLeft.y) / 4 * 3);
        leftPointQuarter[1] = topLeft;
        leftColliderQuarter.points = leftPointQuarter;

        // Crear colisionador para el borde lateral derecho
        EdgeCollider2D rightCollider = edgeColliders.AddComponent<EdgeCollider2D>();
        Vector2[] rightPoints = new Vector2[2];
        rightPoints[0] = bottomRight;
        rightPoints[1] = topRight;
        rightCollider.points = rightPoints;
        
        //Crear colisionador para el borde superior
        EdgeCollider2D topCollider = edgeColliders.AddComponent<EdgeCollider2D>();
        Vector2[] topPoints = new Vector2[2];
        topPoints[0] = topLeft;
        topPoints[1] = topRight;
        topCollider.points = topPoints;
        topCollider.tag = "TopEdge"; // Asignar etiqueta para identificar el borde superior

        //Crear colisionador para la ventana
        EdgeCollider2D windowCollider = edgeColliders.AddComponent<EdgeCollider2D>();
        Vector2[] windowPoint = new Vector2[4];
        windowPoint[0] = new Vector2(bottomLeft.x, bottomLeft.y + (topLeft.y - bottomLeft.y) / 4 * 2);
        windowPoint[1] = new Vector2(bottomLeft.x-1, bottomLeft.y + (topLeft.y - bottomLeft.y) / 4 * 2);
        windowPoint[2] = new Vector2(bottomLeft.x - 1, bottomLeft.y + (topLeft.y - bottomLeft.y) / 4 * 3);
        windowPoint[3] = new Vector2(bottomLeft.x, bottomLeft.y + (topLeft.y - bottomLeft.y) / 4 * 3);
        windowCollider.points = windowPoint;
        windowCollider.tag = "Window"; // Asignar etiqueta para identificar la ventana
    
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
            // Borde lateral izquierdo (mitad inferior)
            Gizmos.DrawLine(bottomLeft, new Vector2(bottomLeft.x, bottomLeft.y + (topLeft.y - bottomLeft.y) / 4*2));
            //Borde lateral izquierdo (cuarto superior)
            Gizmos.DrawLine(new Vector2(bottomLeft.x, bottomLeft.y + (topLeft.y - bottomLeft.y) / 4 * 3), topLeft);
            // Borde lateral derecho (tercio inferior)
            Gizmos.DrawLine(bottomRight, topRight);

            // Borde superior
            Gizmos.DrawLine(topLeft, topRight);

            // Ventana
            Gizmos.color = Color.blue;
            Vector2 windowBottomRight = new Vector2(bottomLeft.x, bottomLeft.y + (topLeft.y - bottomLeft.y) / 4 * 2);
            Vector2 windowBottomLeft = new Vector2(bottomLeft.x - 1, bottomLeft.y + (topLeft.y - bottomLeft.y) / 4 * 2);
            Vector2 windowTopLeft = new Vector2(bottomLeft.x - 1, bottomLeft.y + (topLeft.y - bottomLeft.y) / 4 * 3);
            Vector2 windowTopRight = new Vector2(bottomLeft.x, bottomLeft.y + (topLeft.y - bottomLeft.y) / 4 * 3);

            Gizmos.DrawLine(windowBottomRight, windowBottomLeft);
            Gizmos.DrawLine(windowBottomLeft, windowTopLeft);
            Gizmos.DrawLine(windowTopLeft, windowTopRight);


        }
#endif
    }
}