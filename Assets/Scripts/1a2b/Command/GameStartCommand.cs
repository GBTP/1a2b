using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class GameStartCommand : AbstractCommand<GameStartCommand>
{
    protected override GameStartCommand OnExecute()
    {
        this.SendEvent<GameStartEvent>();
        return this;
    }
}
