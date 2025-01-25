using System;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
