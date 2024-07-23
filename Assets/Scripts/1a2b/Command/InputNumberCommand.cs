using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class InputNumberCommand : AbstractCommand<InputNumberCommand>
{
    private int m_InputNumber;

    public InputNumberCommand(int input)
    {
        m_InputNumber = input;
    }

    protected override InputNumberCommand OnExecute()
    {
        var model = this.GetModel<GameModel>();

        if (!model.PlayerGuess.Value.Contains(m_InputNumber))
        {
            var newlist = new List<int>(model.PlayerGuess.Value);
            newlist.Add(m_InputNumber);
            model.PlayerGuess.Value = newlist;
        }

        return this;
    }
}

