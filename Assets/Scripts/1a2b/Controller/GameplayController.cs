using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour, IController
{
    public IArchitecture GetArchitecture()
    {
        return GameApp.Interface;
    }

    [SerializeField] private Text m_ResultText;

    private void Start()
    {
        this.GetModel<GameModel>().Input.Register(OnPlayerInput);
    }

    private void OnPlayerInput(List<int> input)
    {
        var numberStr = List2String(input);

        if (input.Count == 4)
        {
            // 不知道这样拿命令的执行结果是否符合规则
            var result = this.SendCommand(new CheckInputCommand());
            m_ResultText.text = numberStr + $",{result.A}A{result.B}B";
        }
        else m_ResultText.text = numberStr;
    }

    private string List2String(List<int> list)
    {
        var str = string.Empty;

        foreach (var number in list)
        {
            str += number.ToString();
        }

        return str;
    }
}
