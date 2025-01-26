using UnityEngine;
using UnityEngine.UI;

public class Dirt : MonoBehaviour
{
    [SerializeField] float minForce, maxForce;
    [SerializeField] Vector2 direction;
    [SerializeField] SpriteSelector spriteSelector;
    Rigidbody2D rb;

    public int GetSprite()
    {
        return spriteSelector.selectedSpriteInt;
    }
    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        float force = Random.Range(minForce, maxForce);
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }

    private void Update()
    {
        if (transform.position.y < -25) DestroyDirt();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Bubble>(out Bubble bubble))
        {
            bubble.CollisionWithDirt(this);
        }
        if (other.CompareTag("BottomEdge"))
        {
            //Incrementar el contador en el GameManager
            DestroyDirt();
        }
    }

    public void PushDirt(Vector2 direction, float force){

        rb.AddForce(direction * force, ForceMode2D.Impulse);

    }
    public void EnableDirt()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        transform.SetParent(null);
    }

    public void DisableDirt(Transform bubbleParent)
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        transform.position = bubbleParent.transform.position;
        transform.parent = bubbleParent.transform;
    }

    public void DestroyDirt()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        // DirtGenerator.CreateDirt.Invoke();
        GameManager.instance.RestTry();
    }
}
