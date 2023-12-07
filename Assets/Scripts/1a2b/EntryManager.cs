using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryManager : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("Main");
        }
    }
}
