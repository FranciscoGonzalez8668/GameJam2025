using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField] BoxCollider2D fanZoneColl;
    public float fanPower;
    public float range;

    AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        audioSource.loop = true;
        audioSource.Play();
    }
    private void OnDisable()
    {
        audioSource.Stop();
    }

}
