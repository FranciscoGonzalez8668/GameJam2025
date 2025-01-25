using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField] BoxCollider2D fanZoneColl;
    public float fanPower;
    public float range;

    SoundsSender soundsSender;
    private void Awake()
    {
        soundsSender = GetComponent<SoundsSender>();
    }

    public void PlayFanOnSound() => soundsSender.Play("On");
    public void PlayFanOffSound() => soundsSender.Play("Off");
}
