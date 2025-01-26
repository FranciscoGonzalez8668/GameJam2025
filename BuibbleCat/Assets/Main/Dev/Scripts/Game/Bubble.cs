using System.Collections;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [HideInInspector] public bool available = true;
    [SerializeField] public float bounceForce = 5f;
    public bool Available
    {
        get { return available; }
        set
        {
            available = value;
            if (available)
            {
                // ToolsController.ChangeTool.Invoke(0);
                spriteObj.SetActive(false);
            }
            else
            {
                spriteObj.SetActive(true);
            }
        }
    }
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

    private void Start()
    {
        Available = true;
        spriteObj.SetActive(false);
    }
    private void Update() {
        rb.gravityScale = containedDirt? 0.65f:0.25f;
    }

    public void ShootBubble(Vector2 pos, float force)
    {
        PlayShootSound();
        Available = false;
        transform.position = pos;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }

    private void DestroyBubble()
    {
        PlayPopSound();
        Available = true;
        if (containedDirt)
        {
            containedDirt.EnableDirt();
            containedDirt = null;
        }
    }

    public void CollisionWithDirt(Dirt dirt)
    {
        if (containedDirt) return;
        PlayCapturedSound();
        containedDirt = dirt;
        dirt.DisableDirt(transform);
        StartCoroutine(HitDirt());
        Invulnerability();
        ToolsController.ChangeTool.Invoke(1);
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
        if (Available) return;
        if (other.CompareTag("Point") && containedDirt)
        {
            FakeBubble fakeBubble = other.GetComponentInParent<FakeBubble>();
            fakeBubble.GenerateFakeBubble(transform.position, GetSprite(), containedDirt.GetSprite());

            Destroy(containedDirt.gameObject);
            DestroyBubble();

            GameManager.instance.AddPoint();
            return;
        }
        if (other.CompareTag("RightEdge"))
        {
            Vector2 bounceDirection = Vector2.left;
            rb.AddForce(bounceDirection * bounceForce, ForceMode2D.Impulse);
            return;
        }
        if (other.CompareTag("LeftEdge"))
        {
            Vector2 bounceDirection = Vector2.right;
            rb.AddForce(bounceDirection * bounceForce, ForceMode2D.Impulse);
            return;
        }

        if (isInvulnerable || other.CompareTag("bubbleSafe")) return;

        DestroyBubble();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Available) return;
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
