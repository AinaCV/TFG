using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    private bool animationFinished = false;

    // Llamaremos a esta funci�n cuando la animaci�n haya terminado
    public void AnimationFinished()
    {
        animationFinished = true;
    }

    private void Update()
    {
        if (animationFinished)
        {
            ChangeSceneAfterAnim();
        }
    }

    private void ChangeSceneAfterAnim()
    {
        SceneManager.LoadScene("End");
    }
}
