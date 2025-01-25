using System;
using System.Collections;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [HideInInspector] public bool available = true;
    public bool Available
    {
        get { return available; }
        set
        {
            available = value;
            if (available) ToolsController.ChangeTool.Invoke(0);
            else ToolsController.ChangeTool.Invoke(1);
        }
    }
    [SerializeField] GameObject spriteObj;
    [SerializeField] float lerpTimeOnHitDirt, invulnerabilityTimeOnHitDirt;
    Rigidbody2D rb;
    bool isInvulnerable;
    Dirt containedDirt;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ShootBubble(Vector2 pos, float force)
    {
        Available = false;
        transform.position = pos;
        spriteObj.gameObject.SetActive(true);
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }

    private void DestroyBubble()
    {
        spriteObj.gameObject.SetActive(false);
        Available = true;

        if (containedDirt) containedDirt.EnableDirt();
    }

    public void CollisionWithDirt(Dirt dirt)
    {
        if (Available) return;

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
        if (other.CompareTag("Window"))
        {
            DestroyBubble();
            GameManager.instance.AddPoint();
            return;
        }
        if (isInvulnerable || other.CompareTag("bubbleSafe")) return;

        DestroyBubble();
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
}
