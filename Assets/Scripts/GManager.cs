using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GManager : MonoBehaviour, CanvasCallbackReceiver
{
    public static bool singlePlayer = false;

    public void Execute()
    {
        SceneManager.LoadScene(2);
    }

    public void EndGame()
    {
        FindObjectOfType<CameraCanvas>().FadeOutIn(this);
    }
}
