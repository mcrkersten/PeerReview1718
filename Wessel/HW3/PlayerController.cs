using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int walkSpeed, runSpeed;
    public int jumpHeight;
    public int health;
    public GameObject[] weapons;
    private Rigidbody rb;
    private int speed;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MoveControls();
    }

    public void MoveControls() {
        var movementX = Input.GetAxisRaw("Horizontal");
        var movementY = Input.GetAxisRaw("Vertical");
        if (movementX > 0.5f || movementX < -0.5f) {
            rb.velocity += transform.right * movementX * speed;
        }
        if (movementY > 0.5f || movementY < -0.5f) {
            rb.velocity += transform.forward * movementY* speed;
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            transform.Translate(Vector3.up * jumpHeight);
        }
        if (Input.GetKey(KeyCode.LeftShift)) {
            speed = runSpeed;
        }
        else {
            speed = walkSpeed;
        }
    }

    public void TakeDamage(int damage) {
        health -= damage;
    }

    public GameObject Weapon() {
        float d = Input.GetAxis("Mouse ScrollWheel");
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            return weapons[(int)d];
        }
        else {
            return null;
        }
    }

    private void Behaviour() {

    }

    private void Die() {
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}
