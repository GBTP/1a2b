using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class CheckInputCommand : AbstractCommand<CheckInputCommand>
{
    public int A, B;
    protected override CheckInputCommand OnExecute()
    {
        var model = this.GetModel<GameModel>();

        model.TestCount++;

        var answer = model.Answer;
        var input = model.Input.Value;

        for (var i = 0; i < 4; i++)
        {
            var targetNumber = answer[i];
            var inputNumber = input[i];

            if (targetNumber == inputNumber)
            {
                A++;
            }
            else if (answer.Contains(inputNumber))
            {
                B++;
            }
        }

        if (A == 4)
        {
            this.SendCommand(new GameWinCommand());
        }

        input.Clear();

        return this;
    }
}
