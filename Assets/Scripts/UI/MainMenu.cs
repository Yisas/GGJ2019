using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private bool player1Started = false;
    private bool player2Started = false;
    public GameObject player1Inactive;
    public GameObject player1Active;
    public Text player1ActiveText;
    public GameObject player2Inactive;
    public GameObject player2Active;
    public Text player2ActiveText;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!player1Started)
        {
            if (Input.GetButtonDown("Player 1 Start"))
            {
                player1Started = true;
                player1Active.SetActive(true);
                player1Inactive.SetActive(false);
                player1ActiveText.enabled = false;
            }
        }

        if (!player2Started)
        {
            if (Input.GetButtonDown("Player 2 Start"))
            {
                player2Started = true;
                player2Active.SetActive(true);
                player2Inactive.SetActive(false);
                player2ActiveText.enabled = false;
            }
        }

        if (player1Started && player2Started)
        {
            anim.SetTrigger("start");
        }
    }

    public void StartGame()
    {
        Debug.Log("here");
        SceneManager.LoadScene(1);
    }
}
