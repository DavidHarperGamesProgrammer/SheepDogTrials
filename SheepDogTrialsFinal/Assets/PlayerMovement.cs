using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    
    public float speed;
    float Sensitivity;
    int camIndex;
    public float turnSpeed;

    public Animator animator;
  
	// Use this for initialization
	void Start () {
        speed = 1.0f;
        turnSpeed = 100.0f;
       


	}
	
	// Update is called once per frame
	void Update () {

        camIndex = PlayerPrefs.GetInt("CameraIndex", 0);

        if (camIndex == 0)
        {
            Sensitivity = PlayerPrefs.GetFloat("Sensitivity", 70);

            //float rotation = Input.GetAxis("Horizontal") * speed;
            float translation = Input.GetAxis("Vertical") * speed;

            //transform.Rotate(0, rotation * Time.deltaTime, 0);

            transform.Translate(0, 0, translation * Time.deltaTime);


            transform.Rotate(0, (Input.GetAxis("Mouse X") * Time.deltaTime)*Sensitivity, 0);
        }
        else if(camIndex == 1)
        {
            float rotation = Input.GetAxis("Horizontal") * turnSpeed;
            float translation = Input.GetAxis("Vertical") * speed;

            transform.Rotate(0, rotation * Time.deltaTime, 0);

            transform.Translate(0, 0, translation * Time.deltaTime);
        }


        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }

        

        
	}
}
