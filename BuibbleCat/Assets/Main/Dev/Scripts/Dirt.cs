using UnityEngine;

public class Dirt : MonoBehaviour
{
    [SerializeField] float minForce, maxForce;
    [SerializeField] Vector2 direction;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        float force = Random.Range(minForce, maxForce);
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        transform.position = other.transform.position;
        transform.parent = other.transform;
    }
}
