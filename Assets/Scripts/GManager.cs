using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GManager : MonoBehaviour, CanvasCallbackReceiver
{
    public bool singlePlayer = false;

    public void Execute()
    {
        SceneManager.LoadScene(2);
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndGame()
    {
        FindObjectOfType<CameraCanvas>().FadeOutIn(this);
    }
}
