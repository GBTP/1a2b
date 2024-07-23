using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using UnityEngine.UI;

public class InputButtonController : MonoBehaviour, IController
{
    public Button m_Button;
    public int InputNumber;

    public IArchitecture GetArchitecture()
    {
        return GameApp.Interface;
    }

    private void Start()
    {
        m_Button.onClick.AddListener(() => this.SendCommand(new InputNumberCommand(InputNumber)));
    }
}
