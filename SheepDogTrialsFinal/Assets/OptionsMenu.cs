using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class OptionsMenu : MonoBehaviour {

    public AudioMixer audioMixer;

    public GameObject cam;

    Resolution[] resolutions;

    public Dropdown resolutionDropdown;
    public Dropdown CameraDropdown;

    public Slider FOV;
    public Slider SENSITIVITY;
    public Slider Volume;

    public bool Third = true;
    void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0;  i  <  resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            if (i == 0)
            {
                options.Add(option);

                if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }
            else
            {
                if (option == resolutions[i - 1].width + " x " + resolutions[i - 1].height)
                {

                }
                else
                {
                    options.Add(option);

                    if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                    {
                        currentResolutionIndex = i;
                    }
                }
            }
           
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        
        CameraDropdown.value = PlayerPrefs.GetInt("CameraIndex", 0);
        FOV.value = PlayerPrefs.GetInt("FOV", 60);
        SENSITIVITY.value = PlayerPrefs.GetInt("Sensitivity", 70);
        Volume.value = PlayerPrefs.GetInt("volume", 0);
        



    }
    public  void  SetVolume (float volume)
    {
        int Vol = (int)volume;
        Debug.Log(Vol);
        PlayerPrefs.SetInt("volume", Vol);
        PlayerPrefs.Save();
        audioMixer.SetFloat("volume", volume);
    }

    public void SetFOV (float FOV)
    {
        int fov = (int)FOV;
        PlayerPrefs.SetInt("FOV", fov);
        PlayerPrefs.Save();
    }

    public void SetSensitivity (float Sensitivity)
    {
        int Sens = (int)Sensitivity;
        PlayerPrefs.SetInt("Sensitivity", Sens);
        PlayerPrefs.Save();
    }

    public void  SetQuality (int  qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetCamera (int CameraIndex)
    {
        PlayerPrefs.SetInt("CameraIndex", CameraIndex);
        PlayerPrefs.Save();

        if (CameraIndex == 0)
        {
            cam.SetActive(true);
            
        }
        else if(CameraIndex == 1)
        {
            cam.SetActive(false);
            
        }

        
        
    }


}
