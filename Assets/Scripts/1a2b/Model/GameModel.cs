using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class GameModel : AbstractModel
{
    public int TestCount;
    public List<int> Answer;

    public BindableProperty<List<int>> Input;

    protected override void OnInit()
    {
        TestCount = 0;
        Answer = new();
        Input = new(new List<int>());
    }
}
