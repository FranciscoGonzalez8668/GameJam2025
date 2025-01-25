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

        //GameObject para el borde inferior
        GameObject edgeCollidersBottom = new GameObject("EdgeColliders");
        edgeCollidersBottom.transform.parent = camera.transform;

        // Crear colisionador para el borde inferior
        EdgeCollider2D bottomCollider = edgeCollidersBottom.AddComponent<EdgeCollider2D>();
        Vector2[] bottomPoints = new Vector2[2];
        bottomPoints[0] = bottomLeft;
        bottomPoints[1] = bottomRight;
        bottomCollider.points = bottomPoints;
        bottomCollider.tag = "BottomEdge"; // Asignar etiqueta para identificar el borde inferior



        //GameObject para el borde izquierdo
        GameObject edgeCollidersleft = new GameObject("EdgeColliders");
        edgeCollidersleft.transform.parent = camera.transform;

        // Crear colisionador para el borde lateral izquierdo 
        EdgeCollider2D leftCollider = edgeCollidersleft.AddComponent<EdgeCollider2D>();
        Vector2[] leftPoints = new Vector2[2];
        leftPoints[0] = bottomLeft;
        leftPoints[1] = topLeft;
        leftCollider.points = leftPoints;
        leftCollider.tag = "LeftEdge";



        //GameObject para el borde derecho
        GameObject edgeCollidersRight = new GameObject("EdgeColliders");
        edgeCollidersRight.transform.parent = camera.transform;


        // Crear colisionador para el borde lateral derecho
        EdgeCollider2D rightCollider = edgeCollidersRight.AddComponent<EdgeCollider2D>();
        Vector2[] rightPoints = new Vector2[2];
        rightPoints[0] = bottomRight;
        rightPoints[1] = topRight;
        rightCollider.points = rightPoints;
        rightCollider.tag = "RightEdge"; // Asignar etiqueta para identificar el borde derecho

        //GameObjet para el borde superior
        GameObject edgeCollidersTop = new GameObject("EdgeColliders");
        edgeCollidersTop.transform.parent = camera.transform;
        //Crear colisionador para el borde superior
        EdgeCollider2D topCollider = edgeCollidersTop.AddComponent<EdgeCollider2D>();
        Vector2[] topPoints = new Vector2[2];
        topPoints[0] = topLeft;
        topPoints[1] = topRight;
        topCollider.points = topPoints;
        topCollider.tag = "TopEdge"; // Asignar etiqueta para identificar el borde superior

    
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
            // Borde inferior 
            Gizmos.DrawLine(bottomLeft, bottomRight);
            //Borde lateral izquierdo 
            Gizmos.DrawLine(bottomLeft, topLeft);
            // Borde lateral derecho 
            Gizmos.DrawLine(bottomRight, topRight);
            // Borde superior
            Gizmos.DrawLine(topLeft, topRight);

        }
#endif
    }
}