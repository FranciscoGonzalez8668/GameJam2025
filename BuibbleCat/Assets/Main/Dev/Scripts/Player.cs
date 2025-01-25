using UnityEngine;

public class Player : MonoBehaviour
{
    void Start()
    {
        // Obtener la posición de la cámara
        
        //Camera mainCamera = Camera.main;
        //
        //Debug.Log(mainCamera.transform.position);
        //if (mainCamera != null)
        //{
        //    // Calcular la posición en el borde inferior de la cámara
        //    Vector3 cameraBottomCenter = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0, mainCamera.nearClipPlane));
        //    cameraBottomCenter.z = 0; // Asegurarse de que la posición Z sea 0
        //
        //    // Ajustar la posición Y para que esté en el borde inferior de la cámara
        //    cameraBottomCenter.y = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0, mainCamera.farClipPlane)).y;
        //
        //    // Establecer la posición del jugador
        //    this.transform.position = cameraBottomCenter;
        //}
    }

    void Update()
    {
        // Aquí puedes agregar lógica adicional si es necesario
    }
}