using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class GameModel : AbstractModel
{
    public int TestCount;
    public List<int> Answer;

    public BindableProperty<List<int>> PlayerGuess;

    protected override void OnInit()
    {
        Answer = new();
        PlayerGuess = new(new List<int>());
    }
}
