using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] int tries;
    int points;

    private void Awake()
    {
        instance = this;
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
}
