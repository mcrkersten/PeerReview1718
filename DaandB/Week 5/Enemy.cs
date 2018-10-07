using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public static Enemy instance = null;

	public float minSpeed = 1;
	public float maxSpeed = 5;

	public float speed;

	// Speed variable; Min and max speed (see car script)
	// Target GameObject (player)
	// public retreatDistance (Perhaps a collider could make this unnecessary)

	// Use this for initialization
	void Start () {
		speed = Random.Range(minSpeed, maxSpeed);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
