using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using UnityEngine.UI;

public class InputButtonController : MonoBehaviour, IController
{
    public IArchitecture GetArchitecture()
    {
        return GameApp.Interface;
    }

    public Button m_Button;
    public int InputNumber;

    private void Start()
    {
        m_Button.onClick.AddListener(() => this.SendCommand(new InputNumberCommand(InputNumber)));
    }
}
