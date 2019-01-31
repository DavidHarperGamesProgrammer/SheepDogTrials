using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDScript : MonoBehaviour {

    public TextMeshProUGUI TimeText;
    public float Timer;

    public GameObject MoveText;
    public GameObject PauseText;
    public GameObject InstructionsText;

    bool Moved = false;

    float TextTimer = 20f;

    // Use this for initialization
    void Start () {
        MoveText.SetActive(true);
        PauseText.SetActive(false);
        InstructionsText.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        Timer += Time.deltaTime;
        
        TimeText.text = "YOUR TIME: " + ((int)Timer).ToString();

        if (((Input.GetAxis("Horizontal") > 0) || (Input.GetAxis("Vertical") > 0)) && Moved == false)
        {
            MoveText.SetActive(false);
            PauseText.SetActive(true);
            Moved = true;
        }

        if (PauseText.activeSelf || InstructionsText.activeSelf)
        {
            TextTimer -= Time.deltaTime;
        }

        if (TextTimer < 0 && PauseText.activeSelf)
        {
            PauseText.SetActive(false);
            InstructionsText.SetActive(true);
            TextTimer = 20f;
        }

        if (TextTimer < 0 && InstructionsText.activeSelf)
        {
            InstructionsText.SetActive(false);
        }
    }
}
