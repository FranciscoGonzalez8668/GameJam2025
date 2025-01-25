using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] TMP_Text pointsTxt, triestTxt;
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
}
