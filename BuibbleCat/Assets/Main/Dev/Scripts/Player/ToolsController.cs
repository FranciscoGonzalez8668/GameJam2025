using System;
using UnityEngine;

public class ToolsController : MonoBehaviour
{
    [SerializeField] GameObject bubbleGun, fan;
    SoundsSender soundsSender;
    public static System.Action<int> ChangeTool;
    private bool isFanActive = false;
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleFan();
        }
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
            isFanActive = true;
        }

        bubbleGun.SetActive(tool == 0);
        fan.SetActive(tool == 1);
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
