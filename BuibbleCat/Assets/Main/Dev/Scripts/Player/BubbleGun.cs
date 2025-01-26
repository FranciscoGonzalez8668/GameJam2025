using UnityEngine;

public class BubbleGun : MonoBehaviour
{
    [SerializeField] Bubble bubble;
    [SerializeField] private GameObject bubblePrefab;
    [SerializeField] float shootForce;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            ShootBubble();
            //bubble.ShootBubble(transform.position, shootForce);
        }
    }

    private void ShootBubble()
    {
        GameObject newBubble = Instantiate(bubblePrefab, transform.position,Quaternion.identity);
        Bubble bubbleComponent = newBubble.GetComponent<Bubble>();
        if (bubbleComponent != null)
        {
            bubbleComponent.ShootBubble(transform.position, shootForce);
        }
    }

}
