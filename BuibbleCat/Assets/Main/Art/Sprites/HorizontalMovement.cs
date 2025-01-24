using UnityEngine;

public class HorizontalMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float velocidad = 5f;

    private Rigidbody2D rb;

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearDamping = 10f;
            rb.gravityScale = 0f; //Para que no le afecte la gravedad
        }
        else
        {
            Debug.LogError("El objeto no tiene un Rigibody2D. Asegurate de agregarlo");
        }

    }   // Update is called once per frame
    void Update()
    {

        float movimientoHorizontal = Input.GetAxis("Horizontal");

        Vector2 velocidadDeseada = new Vector2(movimientoHorizontal * velocidad, rb.linearVelocity.y);

        if (rb != null)
        {
            rb.linearVelocity = velocidadDeseada;
        }

    }
}
