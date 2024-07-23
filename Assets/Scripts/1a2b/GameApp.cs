using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class GameApp : Architecture<GameApp>
{
    protected override void Init()
    {
        RegisterModel(new GameModel());
    }
}
