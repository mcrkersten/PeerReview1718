using UnityEngine;
using Assets.Pixelation.Example.Scripts;
using System.Collections;

public class Player : MonoBehaviour {
	//REVOLUTIE VOOR DE HAAKJES PLZ!!!

	private bool lockMouseMovement;
	private MouseLook look;

	private Transform weapon;

    	public int Health {
		get;
		set;
	}

	private Vector3 lastPos;
    	private int hurtDelay = 0;

	private void Start() {
		Health = 5;
		lastPos = transform.position;
	}

	private void Update() {
		if (hurtDelay > 0) hurtDelay++;
		if (hurtDelay > 20) hurtDelay = 0;

		if (lockMouseMovement) look.enabled = false;
		else {
			look.enabled = true;
			if(weapon.GetComponent<Weapon>()) weapon.GetComponent<Weapon>().WeaponUpdate();
		}
	}

	private void FixedUpdate() {
		if(!lockMouseMovement && weapon.GetComponent<Weapon>()) weapon.GetComponent<Weapon>().FixedWeaponUpdate();
	}

	public bool IsMoving() {
		Vector3 disp = transform.position - lastPos;
		lastPos = transform.position;
		return disp.magnitude > 0.001;
	}

	public void Hurt(int i) {
		hurtDelay = 1;
		Health -= i;

		if (Health <= 0) {
		    Health = 0;
		    Die();
		}
	}

	public void Die() {
		lockMouseMovement = true;
		GetComponent<CharacterMotor>().canControl = false;
		GetComponent<MouseLook>().enabled = false;

		transform.Find("Camera/Main Camera/Gun Camera").gameObject.SetActive(false);
    	}
}
