using UnityEngine;

public class Fan : MonoBehaviour
{
    public float fanPower;
    public float range;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(1, range, 1));
    }
}
