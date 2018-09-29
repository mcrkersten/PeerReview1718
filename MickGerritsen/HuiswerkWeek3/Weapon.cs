using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public string name;
    private int strength;
	// Use this for initialization
	void Start () {
		//Set UI text to the name of this weapon
	}
	
	// Update is called once per frame
	void Update () {
        Fire();
	}
    public void Fire() {
        //Detect of the shot game object (with raycast) has a NPC script and damage the health integer of that script
    }
}
