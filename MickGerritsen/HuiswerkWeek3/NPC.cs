using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IDamagable {
    public string name;
    private int level;
    int IDamagable.health { get; set; }
    private bool IsAttackable;
    public Weapon weapon;
    public Transform player;
	// Use this for initialization
	void Start () {
        //Set UI text to the name of this weapon
    }

    // Update is called once per frame
    void Update () {
        Attack();
        Die();
        OnInteract();
        StartConversation();
        Move();
	}

    public void Attack() {
        //raycasts that will check for player
    }
    public void Die() {
        if (health <= 0){Destroy(this.GameObject);}
    }
    public void OnInteract() {
        //checking for mouse hove and click
    }
    public void StartConversation() {
        //UI elements
    }
    public void Move() {
        //Raycast
        //State-system AI that moves according to the distance to the player
    }

    public void TakeDamage(int amount) {
        throw new System.NotImplementedException();
    }
}
