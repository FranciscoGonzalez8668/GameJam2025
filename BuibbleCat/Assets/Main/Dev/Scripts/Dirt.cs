using UnityEngine;

public class Dirt : MonoBehaviour
{
    [SerializeField] float minForce, maxForce;
    [SerializeField] Vector2 direction;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip collisionBubbleClip;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        float force = Random.Range(minForce, maxForce);
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Bubble>() != null)
        {
            //Reproducir Sonido colision
            if (audioSource != null && collisionBubbleClip != null)
            {
                audioSource.PlayOneShot(collisionBubbleClip);
            }

            transform.position = other.transform.position;
            transform.parent = other.transform;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
        else
        {
            Debug.Log("Dirt collision with: " + other.name);
        }
        
    }
}
