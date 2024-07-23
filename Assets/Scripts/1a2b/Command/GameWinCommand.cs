using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class GameWinCommand : AbstractCommand
{
    protected override void OnExecute()
    {
        this.SendEvent<GameWinEvent>();
    }
}
