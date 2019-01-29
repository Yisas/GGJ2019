using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalLevelCanvas : MonoBehaviour
{
    public Image blackoutImage;
    public float fadeSpeed;
    public float timeToRestart;

    private float timer;
    private bool restarting = false;

    // Start is called before the first frame update
    void Start()
    {
        timer = timeToRestart;
        StartCoroutine("FadeInCoroutine");
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0 && !restarting)
        {
            Restart();
            
        }
    }

    IEnumerator FadeInCoroutine()
    {
        Color c;

        for (float f = 1f; f >= 0; f -= fadeSpeed)
        {
            c = blackoutImage.color;
            c.a = f;
            blackoutImage.color = c;
            yield return null;
        }

        c = blackoutImage.color;
        c.a = 0;
        blackoutImage.color = c;
    }

    IEnumerator FadeOutCoroutine()
    {
        Color c;

        for (float f = 0f; f <= 1.0f; f += fadeSpeed)
        {
            c = blackoutImage.color;
            c.a = f;
            blackoutImage.color = c;
            yield return null;
        }

        c = blackoutImage.color;
        c.a = 1;
        blackoutImage.color = c;
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        restarting = true;
        Destroy(FindObjectOfType<SoundManager>().gameObject);
        StartCoroutine("FadeOutCoroutine");
    }
}
