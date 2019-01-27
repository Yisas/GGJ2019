using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalLevelCanvas : MonoBehaviour
{
    public Image blackoutImage;
    public float fadeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("FadeInCoroutine");
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
