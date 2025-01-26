using System;
using UnityEngine;

public class ToolsController : MonoBehaviour
{
    [SerializeField] GameObject bubbleGun, fan;
    SoundsSender soundsSender;
    public static System.Action<int> ChangeTool;
    private bool isFanActive = false;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleFan();
        }
    }

    private void SwitchTool(int tool)
    {
        tool=!tool;
        
        if (tool)
        {
            PlayBubbleGunSound();
        }
        
        if (tool)
        {
            PlayFanBtnSound();
            isFanActive = true;
        }

        bubbleGun.SetActive(tool);
        fan.SetActive(!tool);


    }

    public void ToggleFan()
    {
        isFanActive = !isFanActive;
        fan.SetActive(isFanActive);
        PlayFanBtnSound();
    }

    void PlayFanBtnSound() => soundsSender.Play("fanButton");
    void PlayBubbleGunSound() => soundsSender.Play("bubbleGun");

}
