using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class StartPanelController : MonoBehaviour, IController
{
    public IArchitecture GetArchitecture()
    {
        return GameApp.Interface;
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            gameObject.SetActive(false);
            this.SendCommand(new GameStartCommand());
        }
    }
}
