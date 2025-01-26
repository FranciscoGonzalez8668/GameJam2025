using System;
using UnityEngine;

public class ToolsController : MonoBehaviour
{
    [SerializeField] GameObject bubbleGun, fan;
    SoundsSender soundsSender;
    public static System.Action<int> ChangeTool;

    bool tool=true;
    // private void OnEnable()
    // {
    //     ChangeTool += SwitchTool;
    // }

private void Update() {
    if(Input.GetKeyDown(KeyCode.Q))SwitchTool();
}
    private void Awake()
    {
        soundsSender = GetComponent<SoundsSender>();
        bubbleGun.SetActive(true);
        fan.SetActive(false);
    }

    private void SwitchTool()
    {
        tool=!tool;
        
        if (tool)
        {
            PlayBubbleGunSound();
        }
        
        if (tool)
        {
            PlayFanBtnSound();
        }

        bubbleGun.SetActive(tool);
        fan.SetActive(!tool);


    }

    void PlayFanBtnSound() => soundsSender.Play("fanButton");
    void PlayBubbleGunSound() => soundsSender.Play("bubbleGun");
}
