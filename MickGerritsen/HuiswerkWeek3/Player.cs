using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable {
    private int level;
    private int experience;
    int IDamagable.health { get; set; }
    private int mana;
    private int strength;
    private int magic;
    private int dexterity;
    private int vitality;
    private GameObject gameOver;
    public Weapon weapon;
	// Use this for initialization
	void Start () {
		//Assign GameObject gameOver to the canvas made for Game-Over screen
	}
	
	// Update is called once per frame
	void Update () {
		//Moving
	}
    public void Die() {
        if (IDamagable.health <= 0) {
            //Game Over Screen and make player unable to move because he died
        }
    }

    public void TakeDamage(int amount) {
        throw new System.NotImplementedException();
    }
}
