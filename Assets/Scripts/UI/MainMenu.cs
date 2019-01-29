using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GManager gManager;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        gManager = FindObjectOfType<GManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void CommonPreStart()
    {
        Cursor.visible = false;
        FindObjectOfType<SoundManager>().TransitionToNormal();
        anim.SetTrigger("start");
    }

    public void StartSinglePlayer()
    {
        CommonPreStart();
        GManager.singlePlayer = true;
    }

    public void StartMultiplayer()
    {
        CommonPreStart();
        GManager.singlePlayer = false;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
