using System.Collections;
using System.Collections.Generic;
using IUtils.Singleton;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoSingleton<MainManager>
{
    public List<int> Target;
    public List<int> Input;

    public int TestCount;

    public Text ShowText;

    public GameObject WinWindow;

    public Button Restart;

    protected override void OnAwake()
    {
        Restart.onClick.AddListener(Replay);

        Target = new();
        Input = new();

        RandomNewTarget();
    }

    private void RandomNewTarget()
    {
        Target.Clear();

        while (true)
        {
            var next = Random.Range(0, 10);
            if (Target.Contains(next)) continue;
            Target.Add(next);
            if (Target.Count == 4) break;
        }
    }

    public void TryInput(int input)
    {
        if (Input.Contains(input)) return;

        Input.Add(input);

        if (ShowText.text.Length > 4) ShowText.text = string.Empty;
        ShowText.text += input.ToString();

        if (Input.Count == 4) CheckInput();
    }

    private void Replay()
    {
        Input.Clear();
        RandomNewTarget();
        TestCount = 0;

        WinWindow.SetActive(false);
    }

    private void CheckInput()
    {
        TestCount++;

        var a = 0;
        var b = 0;

        for (var i = 0; i < 4; i++)
        {
            var target = Target[i];
            var input = Input[i];

            if (target == input)
            {
                a++;
            }
            else if (Target.Contains(input))
            {
                b++;
            }
        }

        if (a == 4)
        {
            Debug.Log("你赢了！");
            ShowText.text = $"你赢了！，尝试次数{TestCount}";
            WinWindow.SetActive(true);
            return;
        }

        ShowText.text = $"{Input[0]}{Input[1]}{Input[2]}{Input[3]},{a}A{b}B";

        Debug.Log($"{Input[0]}{Input[1]}{Input[2]}{Input[3]},{a}A{b}B");
        Input.Clear();
    }
}
