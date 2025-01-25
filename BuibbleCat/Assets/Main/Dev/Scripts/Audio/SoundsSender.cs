using System.Linq;
using UnityEngine;

public class SoundsSender : MonoBehaviour
{
    [System.Serializable]
    public class SoundToSend
    {
        public string id;
        [SerializeField] AudioClip[] clips;
        public AudioClip GetRandomClip()
        {
            return clips[Random.Range(0, clips.Length)];
        }
        public AudioClip GetClip(int index) => clips[index];
    }
    
    [SerializeField] SoundToSend[] soundsToSend;

    public void Play(string id)
    {
        SoundToSend sound = soundsToSend.Where(s => s.id == id).FirstOrDefault();
        AudioManager.Instance.PlaySFX(sound.GetRandomClip());
    }
}
