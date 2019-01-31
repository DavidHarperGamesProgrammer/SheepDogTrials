using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;

    public FlockManager myManager;
    

    public GameObject PauseMenuUI;
    public GameObject OptionsMenuUI;
    public GameObject WinUI;
    public GameObject HUDUI;

    public AudioSource Background;
    public AudioSource Menu;

   

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (OptionsMenuUI.activeSelf == false)
            {
                if (isPaused)
                {

                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }

        if (myManager.win)
        {
            Win();
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Background.Play();
        Menu.Pause();
       
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Background.Pause();
        Menu.Play();

    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
        
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("QUIT!");
    }

    void Win()
    {
        WinUI.SetActive(true);
        HUDUI.SetActive(false);
        Time.timeScale = 0f;
    }

    
}
