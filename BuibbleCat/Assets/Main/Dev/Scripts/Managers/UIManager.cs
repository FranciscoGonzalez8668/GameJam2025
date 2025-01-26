using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] TMP_Text pointsTxt, triestTxt;
    [SerializeField] private GameObject gameOverPanel;
    private void Awake()
    {
        instance = this;
    }

    public void UpdatePointsTxt(int points)
    {
        pointsTxt.text = points.ToString();
    }

    public void UpdateTriesTxt(int tries)
    {
        triestTxt.text = tries.ToString();
    }

    public void showGameOverPanel()
    {

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        else
        {

            Debug.LogError("No se ha asignado el panel de game over");
        }
    
    }
}
