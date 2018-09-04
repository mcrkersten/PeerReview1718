/* 
Hey boys die dit nakijken, ik ben vergeten wat er allemaal precies in de Style Guide stond, 
maar ik kon mij nog wel herrineren dan ik uit mezelf toch al redelijk volgens onze style
guide werkte. Hoop dat het zo goed is? Anders pas ik het nog wel aan zodra de style guide
online gezet wordt. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerController : MonoBehaviour {

    public float speed = 10.0f;
    public GameObject armPickup;
    public GameObject player;

    // Use this for initialization
    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    // Update is called once per frame
    void Update(){
        float translation = Input.GetAxis("Vertical") * speed;
        float straffe = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;

        transform.Translate(straffe, 0, translation);

        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;     
    }


    void OnTriggerEnter(Collider other){
        if (other.tag == "arm"){
            Destroy(armPickup);
            player.GetComponent<ArmScript>().enabled = true;
        }
    }
}