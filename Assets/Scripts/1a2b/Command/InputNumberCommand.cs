using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class InputNumberCommand : AbstractCommand
{
    private int m_InputNumber;

    public InputNumberCommand(int input)
    {
        m_InputNumber = input;
    }

    protected override void OnExecute()
    {
        var model = this.GetModel<GameModel>();

        // 为了触发set有点抽象了
        if (!model.Input.Value.Contains(m_InputNumber))
        {
            var newlist = new List<int>(model.Input.Value);
            newlist.Add(m_InputNumber);
            model.Input.Value = newlist;
        }
    }
}

