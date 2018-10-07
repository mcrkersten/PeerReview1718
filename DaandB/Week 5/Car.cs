using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Car : Enemy {

	public Rigidbody2D rigBod;

    public float rotationSpeed = 5;
	
	void Awake(){
		StartCoroutine(CarRot());
	}

	void FixedUpdate(){
		Vector2 forward = new Vector2(transform.right.x, transform.right.y);

		rigBod.MovePosition(rigBod.position + forward * Time.fixedDeltaTime * speed);
	}

	private IEnumerator CarRot(){
		yield return new WaitForSeconds(0.5f);
		transform.Rotate(0f, 0f, Random.Range(-15f, 15f));
	}

	private void OnTriggerEnter(Collider other){
		PlayerDeath.playerDeath(car); // Research. (Reviewers negeer dit maar. Het werkt momenteel niet maar het probleem ligt bij een anders script).
	}
}
