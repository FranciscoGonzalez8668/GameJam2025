using System;
using UnityEngine;

public class ToolsController : MonoBehaviour
{
    [SerializeField] GameObject bubbleGun, fan;
    SoundsSender soundsSender;
    public static System.Action<int> ChangeTool;
    private void OnEnable()
    {
        ChangeTool += SwitchTool;
    }

    private void Awake()
    {
        soundsSender = GetComponent<SoundsSender>();
        bubbleGun.SetActive(true);
        fan.SetActive(false);
    }

    private void SwitchTool(int tool)
    {
        if (tool == 0)
        {
            PlayBubbleGunSound();
        }
        
        if (tool == 1)
        {
            PlayFanBtnSound();
        }

        bubbleGun.SetActive(tool == 0);
        fan.SetActive(tool == 1);
    }

    void PlayFanBtnSound() => soundsSender.Play("fanButton");
    void PlayBubbleGunSound() => soundsSender.Play("bubbleGun");
}
