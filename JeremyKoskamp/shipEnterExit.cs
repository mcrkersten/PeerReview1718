using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipEnterExit : MonoBehaviour {

    public Transform sitPos;
    public Transform exitPos;
    public GameObject player;

    public bool inBoat{
        get{ return inBoat; }
        set{ inBoat = value; }
    }

    public float timeToInsure{
        get { return timeToInsure; }
        set { timeToInsure = value; }
    }

    


    void Start(){
        inBoat = false; //Bool for checking if player is in the boat.
    }


    void Update(){
        //Exit Boat on keypress
        if (Input.GetKey(KeyCode.E) && inBoat == true){
            player.GetComponent<Rigidbody>().WakeUp();
            player.GetComponent<characterController>().enabled = true;
            player.GetComponent<Transform>().position = exitPos.position;
            player.gameObject.transform.parent = null;
            inBoat = false;
        }
    }


    void OnTriggerEnter(Collider other){
        if(other.tag == "exitPos"){
            exitPos = other.GetComponentInParent<Transform>();
        }
    }


    void OnTriggerStay (Collider other){
        //Checks if colission is with player.
        if(other.tag == "Player"){
            //Enter Boat on keypress
            if (Input.GetKey(KeyCode.E) && inBoat == false){
                other.GetComponent<Transform>().position = sitPos.transform.position + new Vector3(0,1,0);
                other.GetComponent<Transform>().rotation = sitPos.transform.rotation;
                other.gameObject.transform.parent = this.gameObject.transform;
                other.GetComponent<characterController>().enabled = false;
                other.GetComponent<Rigidbody>().Sleep();
                StartCoroutine(Waiting());
            }
        }
    }


    //Timer to prevent instant exiting.
    IEnumerator Waiting(){
        yield return new WaitForSeconds(timeToInsure);
        inBoat = true;
    }
}
