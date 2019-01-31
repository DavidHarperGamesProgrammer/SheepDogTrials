using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;



public class MainMenu : MonoBehaviour {

    public AudioMixer audioMixer;
    public Slider Volume;

    void Start()
    {
        audioMixer.SetFloat("volume", PlayerPrefs.GetInt("volume", 0));
    }

    public void PlayGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame ()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
