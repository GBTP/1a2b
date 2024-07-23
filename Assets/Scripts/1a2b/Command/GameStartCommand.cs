using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class GameStartCommand : AbstractCommand
{
    protected override void OnExecute()
    {
        // 生成新的答案
        this.SendCommand(new GenerateAnswerCommand());
        // 尝试次数清零
        this.GetModel<GameModel>().TestCount = 0;
    }
}
