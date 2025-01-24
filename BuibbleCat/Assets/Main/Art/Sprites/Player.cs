using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    void Start()
    {
        // Obtener la posición de la cámara
        //Camera mainCamera = Camera.main;
        //if (mainCamera != null)
        //{
        //    // Calcular la posición central en el borde inferior de la cámara
        //    Vector3 cameraBottomCenter = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0, mainCamera.nearClipPlane));
        //    cameraBottomCenter.z = 0; // Asegurarse de que la posición Z sea 0
        //
        //    // Ajustar la posición Y para que esté en el borde inferior de la cámara
        //    cameraBottomCenter.y = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0, mainCamera.nearClipPlane)).y;
        //
        //    // Establecer la posición del jugador
        //    transform.position = cameraBottomCenter;
        //}
    }  // Update is called once per frame
    void Update()
    {
        

    }
}
