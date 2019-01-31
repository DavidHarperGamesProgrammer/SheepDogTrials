using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class winScript : MonoBehaviour {

    public HUDScript Hud;
    public TextMeshProUGUI TimeText;
    public TextMeshProUGUI TimeText2;
    public TMP_InputField inputField;

    // Use this for initialization
    void Start () {
		TimeText.text = TimeText2.text + " Seconds.";
	}
	
	public void ClearInput()
    {
        inputField.placeholder.GetComponent<TMP_Text>().text = "";
    }

    public void SetName(string Name)
    {
        if ((int)Hud.Timer < PlayerPrefs.GetInt("HighScore", 1000))
        {
            PlayerPrefs.SetInt("HighScore", (int)Hud.Timer);
            PlayerPrefs.SetString("Name", Name);
            PlayerPrefs.Save();
        }
    }
}
