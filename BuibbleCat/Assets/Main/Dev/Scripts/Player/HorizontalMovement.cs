using UnityEngine;

public class HorizontalMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;
    public float velocidad = 5f;
    public float dashForce = 80f;
    public float dashDuration = 0.5f;
    private bool isDashing = false;
    private float dashTime;


    private Rigidbody2D rb;
    SoundsSender soundsSender;
    private void Awake()
    {
        soundsSender = GetComponent<SoundsSender>();
    }
    void Start()
    {
        try
        {
            rb = GetComponent<Rigidbody2D>();
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error al obtener el componente Rigidbody2D: " + ex.Message);
        }


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

        if (isDashing)
        {
            dashTime -= Time.deltaTime;
            if (dashTime <= 0)
            {
                isDashing = false;
                rb.linearVelocity = Vector2.zero;
            }
            return;
        }

        float movimientoHorizontal = Input.GetAxis("Horizontal");
        //float movimientoVertical = Input.GetAxis("Vertical");
        Vector2 velocidadDeseada = new Vector2(movimientoHorizontal * velocidad, rb.linearVelocityY);

        animator.SetFloat("Walk", movimientoHorizontal == 0 ? 0 : 1);
        spriteRenderer.flipX = movimientoHorizontal > 0 ? false : true;
        rb.linearVelocity = velocidadDeseada;

        if (rb != null)
        {
            rb.linearVelocity = velocidadDeseada;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && movimientoHorizontal != 0 && !isDashing)
        {
            Dash(movimientoHorizontal);
        }

    }

    void PlayStepSound() => soundsSender.Play("step");

    private void Dash(float direction)
    {
        isDashing = true;
        dashTime = dashDuration;
        Vector2 dashVelocity = new Vector2(direction * dashForce, 0);
        rb.linearVelocity = dashVelocity;
    }

}
