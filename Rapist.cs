using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rapist : MonoBehaviour {


    private float WalkUp = 0.8f;            //timer tijd wanneer voordat de rapist je achterna gaat

    private bool walk;
    private bool hi;
    private bool mybad;

    public GameObject helloText;
	public GameObject sorryText;

	public float speed;
	public float velocity;


	public LayerMask whatisobstacle;

	public Rigidbody2D GuyRigidbody;
	public Animator TheAnimator;
	public GameObject Traps;
	public GameObject Obstacles;

	public Collider2D RapeCollider;
	

	void Start () {


		GuyRigidbody = GetComponent<Rigidbody2D>();

		RapeCollider = GetComponent<Collider2D>();

		TheAnimator = GetComponent<Animator>();

		walk = true;


	}


	void Update () {
		

		TheAnimator.SetFloat ("Speed2", GuyRigidbody.velocity.x);       
		WalkUp -= Time.deltaTime;                   //Counts down the timer


		if (WalkUp < -4) {                           //Stops movement when timer is at 0 and starts it again when timer is at -4             
			walk = true;
            helloText.SetActive(true);
        }
        else if (WalkUp < 0) {
            walk = false;
            helloText.SetActive(false);
        }

			
		if (walk) {
			GuyRigidbody.velocity = new Vector2 (speed, GuyRigidbody.velocity.y);
		} else {
			GuyRigidbody.velocity = new Vector2 (0, 0);
		}


		if (mybad) {
			sorryText.SetActive (true);
		}
	}


	void OnCollisionEnter2D (Collision2D col){


		if (col.gameObject.tag == "Player") {       //When the rapist collides with the player, he stops moving and an sorry sign appears.
			//print ("Hit");
			walk = false;                           // stops moving to the right
			mybad = true;                           // sorry text appears
		}


	}
}
