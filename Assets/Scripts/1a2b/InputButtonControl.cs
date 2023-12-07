using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputButtonControl : MonoBehaviour
{
    public Button m_Button;
    public int InputNumber;

    private void Awake()
    {
        m_Button.onClick.AddListener(() => MainManager.Instance.TryInput(InputNumber));
    }
}
