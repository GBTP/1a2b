using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using UnityEngine.UI;

public class GameWinPanelController : MonoBehaviour, IController
{
    [SerializeField] private GameObject m_Panel;
    [SerializeField] private Text m_TestCountText;
    [SerializeField] private Button m_RestartButton;

    public IArchitecture GetArchitecture()
    {
        return GameApp.Interface;
    }

    private void Start()
    {
        this.RegisterEvent<GameWinEvent>(e =>
        {
            m_Panel.SetActive(true);
            m_TestCountText.text = "你赢了！尝试次数：" + this.GetModel<GameModel>().TestCount.ToString();
        });


        m_RestartButton.onClick.AddListener(() =>
        {
            m_Panel.SetActive(false);
            this.SendCommand(new GameStartCommand());
        });
    }
}
