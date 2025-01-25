using System;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ShootBubble(Vector2 pos, float force)
    {
        transform.position = pos;
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }

    private void DestroyBubble()
    {
        this.gameObject.SetActive(false);
        DirtGenerator.CreateDirt.Invoke();
        gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent<Fan>(out Fan fan))
        {
            float distance = Vector2.Distance(transform.position, fan.transform.position);
            float force = fan.fanPower * ((fan.range - distance) < 0 ? 0 : fan.range - distance);

            Vector2 toOther = (other.transform.position - transform.position).normalized;
            float dot = Vector2.Dot(other.transform.up, toOther);

            // Debug.Log("bubble force received: " + force);
            rb.AddForce(new Vector2(dot, 1).normalized * force, ForceMode2D.Force);
        }
    }
}
