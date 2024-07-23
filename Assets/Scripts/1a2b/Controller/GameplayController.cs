using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour, IController
{
    public IArchitecture GetArchitecture()
    {
        return GameApp.Interface;
    }

    private GameModel m_Model;

    [SerializeField] private Text m_ResultText;

    private void Start()
    {
        this.RegisterEvent<GameStartEvent>(e =>
        {
            StartGame();
        }).UnRegisterWhenGameObjectDestroyed(gameObject);

        m_Model = this.GetModel<GameModel>();

        m_Model.PlayerGuess.Register(OnPlayerInput);
    }

    private void StartGame()
    {
        RandomNewTarget();
    }

    private void RandomNewTarget()
    {
        m_Model.Answer.Clear();

        while (true)
        {
            var next = Random.Range(0, 10);
            if (m_Model.Answer.Contains(next)) continue;
            m_Model.Answer.Add(next);
            if (m_Model.Answer.Count == 4) break;
        }
    }

    private void OnPlayerInput(List<int> input)
    {
        if (input.Count == 4)
        {
            CheckInput();
        }
        else m_ResultText.text = List2String(input);
    }

    private string List2String(List<int> list)
    {
        var str = string.Empty;

        foreach (var number in list)
        {
            str += number.ToString();
        }

        return str;
    }

    private void CheckInput()
    {
        m_Model.TestCount++;

        var a = 0;
        var b = 0;

        var answer = m_Model.Answer;
        var input = m_Model.PlayerGuess.Value;

        for (var i = 0; i < 4; i++)
        {
            var target = answer[i];
            var inputNum = input[i];

            if (target == inputNum)
            {
                a++;
            }
            else if (answer.Contains(inputNum))
            {
                b++;
            }
        }

        if (a == 4)
        {
            this.SendCommand(new GameWinCommand());
        }

        m_ResultText.text = $"{input[0]}{input[1]}{input[2]}{input[3]},{a}A{b}B";

        input.Clear();
    }
}
