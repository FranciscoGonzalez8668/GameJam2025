using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    void Start()
    {
        // Obtener la posici�n de la c�mara
        //Camera mainCamera = Camera.main;
        //if (mainCamera != null)
        //{
        //    // Calcular la posici�n central en el borde inferior de la c�mara
        //    Vector3 cameraBottomCenter = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0, mainCamera.nearClipPlane));
        //    cameraBottomCenter.z = 0; // Asegurarse de que la posici�n Z sea 0
        //
        //    // Ajustar la posici�n Y para que est� en el borde inferior de la c�mara
        //    cameraBottomCenter.y = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0, mainCamera.nearClipPlane)).y;
        //
        //    // Establecer la posici�n del jugador
        //    transform.position = cameraBottomCenter;
        //}
    }  // Update is called once per frame
    void Update()
    {
        

    }
}
