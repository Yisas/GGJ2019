using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraCanvas : MonoBehaviour
{
    public Image panicProgressBar;
    public float panicProgressPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePanicLevel(float percentage)
    {
        Vector3 rectPosition = panicProgressBar.rectTransform.localPosition;
        rectPosition.x = -panicProgressPos + panicProgressPos * (percentage);
        panicProgressBar.rectTransform.localPosition = rectPosition;
        panicProgressBar.rectTransform.localScale = new Vector3(percentage, 1, 1);
    }
}
