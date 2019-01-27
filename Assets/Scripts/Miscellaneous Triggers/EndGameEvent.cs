using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameEvent : MonoBehaviour
{
    public Light directionalLight;
    public float finalIntensity;
    public float timeToFinalIntensity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine("RaiseIntensity");
            other.GetComponent<Steering>().GoToEndGame();
            other.GetComponent<Steering>().affectedByWind = false;
        }
    }

    IEnumerator RaiseIntensity()
    {
        float intesity = directionalLight.intensity;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / timeToFinalIntensity)
        {
            intesity = Mathf.Lerp(intesity, finalIntensity, t);
            directionalLight.intensity = intesity;
            yield return null;
        }
    }
}
