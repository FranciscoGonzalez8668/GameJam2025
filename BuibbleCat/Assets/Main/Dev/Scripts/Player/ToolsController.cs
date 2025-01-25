using System;
using UnityEngine;

public class ToolsController : MonoBehaviour
{
    [SerializeField] GameObject bubbleGun, fan;
    public static System.Action<int> ChangeTool;
    bool isCurrentFan;
    private void OnEnable()
    {
        ChangeTool += SwitchTool;
    }

    private void Awake()
    {
        bubbleGun.SetActive(true);
        fan.SetActive(false);
    }

    private void SwitchTool(int tool)
    {
        isCurrentFan = !isCurrentFan;

        bubbleGun.SetActive(tool == 0);
        fan.SetActive(tool == 1);
    }
}
