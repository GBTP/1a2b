using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class GenerateAnswerCommand : AbstractCommand
{
    protected override void OnExecute()
    {
        var answer = this.GetModel<GameModel>().Answer;
        answer.Clear();

        while (true)
        {
            var next = Random.Range(0, 10);
            if (answer.Contains(next)) continue;
            answer.Add(next);
            if (answer.Count == 4) break;
        }
    }
}
