using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField] BoxCollider2D fanZoneColl;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip fanClip;
    public float fanPower;
    public float range;


    private void OnEnable()
    {

        //Reproducir el sonido del ventilador
        if (audioSource != null && fanClip != null)
        {
            audioSource.clip = fanClip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    private void OnDisable()
    {
        //Detener el sonido del ventilador
        if (audioSource != null)
        {
            audioSource.Stop();
        }

    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     Vector2 from = transform.position;
    //     from.y -= range;
    //     Vector2 to = from;
    //     to.y += range;
    //     Gizmos.DrawLine(from, to);
    // }
}
