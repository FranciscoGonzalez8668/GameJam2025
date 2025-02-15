using System.Collections;
using System.ComponentModel;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] public float bounceForce = 5f;
    [SerializeField] GameObject spriteObj;
    [SerializeField] float lerpTimeOnHitDirt, invulnerabilityTimeOnHitDirt;
    [SerializeField] SpriteSelector spriteSelector;
    SoundsSender soundsSender;
    Rigidbody2D rb;
    bool isInvulnerable;
    Dirt containedDirt;

    public int GetSprite()
    {
        return spriteSelector.selectedSpriteInt;
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        soundsSender = GetComponent<SoundsSender>();
    }
    private void Update()
    {
        rb.gravityScale = containedDirt ? 0.65f : 0.25f;
    }

    public void ShootBubble(Vector2 pos, float force)
    {
        PlayShootSound();
        transform.position = pos;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }

    private void DestroyBubble()
    {
        PlayPopSound();
        if (containedDirt)
        {
            containedDirt.EnableDirt();
            containedDirt = null;
        }
        Destroy(gameObject);
    }

    public void CollisionWithDirt(Dirt dirt)
    {
        if (containedDirt) return;
        PlayCapturedSound();
        containedDirt = dirt;
        dirt.DisableDirt(transform);
        StartCoroutine(HitDirt());
        Invulnerability();
    }

    public void Invulnerability() => StartCoroutine(InvulnerabilityCoroutine());

    IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;
        float actualTime = invulnerabilityTimeOnHitDirt;
        while (actualTime > 0)
        {
            actualTime -= Time.deltaTime;
            yield return null;
        }
        isInvulnerable = false;
    }

    IEnumerator HitDirt()
    {
        float lerpTime = lerpTimeOnHitDirt;
        float actualVelocity = rb.linearVelocityY;
        while (lerpTime > 0)
        {
            lerpTime -= Time.deltaTime;
            rb.linearVelocityY = Mathf.Lerp(0, actualVelocity, lerpTime / lerpTimeOnHitDirt);
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector2 bounceDirection = Vector2.right;
        switch (other.tag)
        {
            case "RightEdge":
                bounceDirection = Vector2.left;
                BounceDirt(bounceDirection);
                break;
            case "LeftEdge":
                bounceDirection = Vector2.right;
                BounceDirt(bounceDirection);
                break;
            case "Point":
                if (containedDirt)
                {
                    FakeBubble fakeBubble = other.GetComponentInParent<FakeBubble>();
                    fakeBubble.GenerateFakeBubble(transform.position, GetSprite(), containedDirt.GetSprite());

                    Destroy(containedDirt.gameObject);
                    DestroyBubble();

                    GameManager.instance.AddPoint();
                    return;
                }
                break;
        }

        if (isInvulnerable || other.CompareTag("bubbleSafe"))
        {
            Vector2 direction = (transform.position - other.transform.position).normalized;
            rb.AddForce(direction * bounceForce, ForceMode2D.Impulse);
            return;
        }

        DestroyBubble();
    }
    
    private void BounceDirt(Vector2 direction)
    {
        if (containedDirt)
        {
            containedDirt.EnableDirt();
            containedDirt.PushDirt(direction, bounceForce);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent<Fan>(out Fan fan))
        {
            float distance = Vector2.Distance(transform.position, fan.transform.position);
            float force = fan.fanPower * ((fan.range - distance) < 0 ? 0.5f : fan.range - distance);

            Vector2 toOther = (other.transform.position - transform.position).normalized;
            float dot = Vector2.Dot(other.transform.up, toOther);

            // Debug.Log("bubble force received: " + force);
            rb.AddForce(new Vector2(dot, 1).normalized * force, ForceMode2D.Force);
        }
    }

    void PlayPopSound() => soundsSender.Play("pop");
    void PlayCapturedSound() => soundsSender.Play("captured");
    void PlayShootSound() => soundsSender.Play("shoot");
}
