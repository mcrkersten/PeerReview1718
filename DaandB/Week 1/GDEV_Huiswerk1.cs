using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerController : MonoBehaviour {

    [SerializeField] private float _speed = 10f;
    public float Speed {
        get { return (_speed); }
        set { Speed = value; }
    }
    public GameObject armPickup { get; set; }
    public GameObject player { get; set; }
    public float translation { get; set; }
    public float straffe { get; set; }

    // Use this for initialization
    private void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    // Update is called once per frame
    private void Update(){
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