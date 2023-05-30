using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("Menu");
    }

    //public void NewGamebutton()
    //{
    //    SceneManager.LoadScene("Game");
    //}
}
