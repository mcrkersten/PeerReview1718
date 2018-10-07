using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEnemy : MonoBehaviour {

    public int walkSpeed, runSpeed;
    public int jumpHeight;
    public int health;
    public GameObject weapon;
    private Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    public void Movement(float speed) {
        //blahblah AI
    }

    public void TakeDamage(int damage) {
        health -= damage;
    }

    public virtual GameObject Weapon() {
        return weapon;
    }

    public void Die() {
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}
