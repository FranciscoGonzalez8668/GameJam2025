using UnityEngine;

public class Player : MonoBehaviour
{
    void Start()
    {
        // Obtener la posici�n de la c�mara
        
        //Camera mainCamera = Camera.main;
        //
        //Debug.Log(mainCamera.transform.position);
        //if (mainCamera != null)
        //{
        //    // Calcular la posici�n en el borde inferior de la c�mara
        //    Vector3 cameraBottomCenter = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0, mainCamera.nearClipPlane));
        //    cameraBottomCenter.z = 0; // Asegurarse de que la posici�n Z sea 0
        //
        //    // Ajustar la posici�n Y para que est� en el borde inferior de la c�mara
        //    cameraBottomCenter.y = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0, mainCamera.farClipPlane)).y;
        //
        //    // Establecer la posici�n del jugador
        //    this.transform.position = cameraBottomCenter;
        //}
    }

    void Update()
    {
        // Aqu� puedes agregar l�gica adicional si es necesario
    }
}