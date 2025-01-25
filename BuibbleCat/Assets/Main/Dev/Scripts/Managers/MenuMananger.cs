using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuMananger : MonoBehaviour
{

    public GameObject optionsPanel;
    public GameObject creditsPanel;

    public void Start()
    {
        if (optionsPanel == null)
        {
            Debug.Log("No se asigno el optionsPanel en el inspector");
        }
        optionsPanel.SetActive(false);

        if (creditsPanel == null)
        {
            Debug.Log("No se asigno el creditsPanl en el inspector");
        }
        creditsPanel.SetActive(false);
    }


    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {

        optionsPanel.SetActive(false);
    }

    public void OpenCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void CloseCredits()
    {
        creditsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }

}
