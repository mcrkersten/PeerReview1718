using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerController : MonoBehaviour {

    public float speed = 10.0f;
    public GameObject armPickup;
    public GameObject player;
    public float translation;
    public float straffe;

    // Use this for initialization
    public void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    // Update is called once per frame
    public void Update(){
        translation = Input.GetAxis("Vertical") * speed;
        straffe = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;

        transform.Translate(straffe, 0, translation);

        if (Input.GetKeyDown("escape")) Cursor.lockState = CursorLockMode.None;     
    }


    public void OnTriggerEnter(Collider other){
        if (other.tag == "arm"){
            Destroy(armPickup);
            player.GetComponent<ArmScript>().enabled = true;
        }
    }
}