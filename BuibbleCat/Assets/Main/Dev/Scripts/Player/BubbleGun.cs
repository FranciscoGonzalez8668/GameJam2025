using UnityEngine;

public class BubbleGun : MonoBehaviour
{
    [SerializeField] Bubble bubble;
    [SerializeField] float shootForce;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && bubble.available)
        {
            bubble.ShootBubble(transform.position, shootForce);
        }
    }
}
