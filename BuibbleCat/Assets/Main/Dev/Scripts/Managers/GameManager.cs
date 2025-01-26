using UnityEngine;
using UnityEngine.Rendering;
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
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

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

        if (tries <= 0)
        {
            tries = 0;
            EndGame();
        }
        UIManager.instance.UpdateTriesTxt(tries);
    }

    private void EndGame()
    {

        Debug.Log("GAME OVER");

        //Show loose UI 

        UIManager.instance.showGameOverPanel();
        //Pausar la musica
        ambientAudioSource.Pause();

        //Pausar Generador de tierra
        DirtGenerator.OnChangeStartState.Invoke(false);

    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
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
