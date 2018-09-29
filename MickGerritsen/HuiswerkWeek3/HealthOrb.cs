using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOrb : MonoBehaviour {
    public int healthpoints;

    public void Heal() {
        this.gameObject.GetComponent<Player>().health += healthpoints;
    }

    //At the moment the orb touches the player, the player will get the amount of healthpoints added to its health
    public void OnCollisionEnter(Collision col) {
        if (col.collider.GetComponent<Player>() != null) {
            Heal();
        }
    }
}
