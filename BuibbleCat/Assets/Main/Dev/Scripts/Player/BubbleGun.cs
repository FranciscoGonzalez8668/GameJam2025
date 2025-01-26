using UnityEngine;

public class BubbleGun : MonoBehaviour
{
    [SerializeField] private GameObject bubblePrefab;
    [SerializeField] float shootForce;
    [SerializeField] float shootInterval;

    private float actualShootInterval;
    private void Update()
    {
        if (actualShootInterval > 0)
        {
            actualShootInterval -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && actualShootInterval <= 0)
        {
            ShootBubble();
        }
    }

    private void ShootBubble()
    {
        actualShootInterval = shootInterval;
        GameObject newBubble = Instantiate(bubblePrefab, transform.position, Quaternion.identity, null);
        Bubble bubbleComponent = newBubble.GetComponent<Bubble>();
        if (bubbleComponent != null)
        {
            bubbleComponent.ShootBubble(transform.position, shootForce);
        }
    }

}
