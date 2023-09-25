using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("InGame");
    }

    public void EndButton()
    {
        Application.Quit();
    }
}
