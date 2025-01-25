using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] int tries;
    int points;

    [Header("Audio Settings")]
    [SerializeField] AudioSource ambientAudioSource;
    [SerializeField] AudioClip ambientClip;

    private void Awake()
    {
        instance = this;

        //Configuracion del audio ambiental
        if (ambientAudioSource != null && ambientClip != null)
        {
            ambientAudioSource.clip = ambientClip;
            ambientAudioSource.loop = true;
            ambientAudioSource.Play();
        }
    }

    public void AddPoint()
    {
        points++;
        UIManager.instance.UpdatePointsTxt(points);
    }

    public void RestTry()
    {
        tries--;
        UIManager.instance.UpdateTriesTxt(tries);
        
        if (tries <= 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        // SHOW LOSE UI
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void SetAmbientVolume(float volume)
    {
        if (ambientAudioSource != null)
        {
            ambientAudioSource.volume = volume;
        }

    }
}
