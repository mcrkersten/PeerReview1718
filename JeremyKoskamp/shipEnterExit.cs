using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipEnterExit : MonoBehaviour {

    public Transform sitPos;
    public Transform exitPos;

    public bool inBoat;
    public float timeToInsure;

    public GameObject player;

    void Start()
    {
        inBoat = false;
    }

    void Update()
    {
        //Exit Boat
        if (Input.GetKey(KeyCode.E) && inBoat == true)
        {
            //player.gameObject.AddComponent<Rigidbody>();
            player.GetComponent<Rigidbody>().WakeUp();
            player.GetComponent<characterController>().enabled = true;
            player.GetComponent<Transform>().position = exitPos.position;
            player.gameObject.transform.parent = null;
            inBoat = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "exitPos")
        {
            exitPos = other.GetComponentInParent<Transform>();
        }
    }

    void OnTriggerStay (Collider other)
    {
        if(other.tag == "Player")
        {
            //Enter Boat
            if (Input.GetKey(KeyCode.E) && inBoat == false)
            {
                other.GetComponent<Transform>().position = sitPos.transform.position + new Vector3(0,1,0);
                other.GetComponent<Transform>().rotation = sitPos.transform.rotation;
                other.gameObject.transform.parent = this.gameObject.transform;
                other.GetComponent<characterController>().enabled = false;
                other.GetComponent<Rigidbody>().Sleep();
                StartCoroutine(Waiting());
            }
        }
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(timeToInsure);
        inBoat = true;
    }

}
