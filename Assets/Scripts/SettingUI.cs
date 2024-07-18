using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [SerializeField]
    private Button mouseControlButton; // 마우스로 조작 버튼

    [SerializeField]
    private Button keyboardMouseControlButton; // 마우스+키보드로 조작 버튼
    private Animator animator; 

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        switch (PlayerSettings.controlType) // 플레이어 셋팅에서 컨트롤 타입이 뭐로 설정되어 있는지
        {
            case EControlType.Mouse: // 만약에 마우스로 설정되어 있으면 마우스 버튼쪽 색상변경
                mouseControlButton.image.color = Color.green;
                keyboardMouseControlButton.image.color = Color.white;
                break;
            case EControlType.KeyboardMouse: // 만약에 마우스+키보드로 설정되어 있으면 마우스+키보드 버튼쪽 색상변경
                mouseControlButton.image.color = Color.white;
                keyboardMouseControlButton.image.color = Color.green;
                break;
        }
    }

    public void SetControlMode (int controlType)
    {
        PlayerSettings.controlType = (EControlType)controlType;
        switch (PlayerSettings.controlType)
        {
            case EControlType.Mouse:
                mouseControlButton.image.color = Color.green;
                keyboardMouseControlButton.image.color = Color.white;
                break;
            case EControlType.KeyboardMouse:
                mouseControlButton.image.color = Color.white;
                keyboardMouseControlButton.image.color = Color.green;
                break;
        }
    }

    public void Close()
    {
        StartCoroutine(CloseAfterDelay());
    }

    private IEnumerator CloseAfterDelay()
    {
        animator.SetTrigger("Close");
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        animator.ResetTrigger("Close");
    }
}
