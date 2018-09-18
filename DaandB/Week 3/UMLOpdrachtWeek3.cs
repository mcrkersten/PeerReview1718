﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed;
    public GameObject target;

    private void Die() {
        // Move enemy back to it's spawnpoint (of hoe je het ook wilt gaan doen)
    }

    private void MoveTowardsPlayer() {
        // Move the ememy toward the player.

    }

    private void EatPlayer() {
        // -1 to player lives
        // if the player's lives are reduced to 0, Player's Die() funcion is called.
    }
}

public class Ghost : Enemy {

    private void Respawn() {
        // Respawns the Ghost at its designated spawn point
    }
}

public class BossGhost : Enemy {

    public int health;
    public float speed;
    public int pelletsEaten;

    private void PelletCount() {
        // Counts the amount of pellets eaten by the boss
    }

    private void GroundPount() {
        // Has the boss perform his Groundpound ability
        // Stun the player (?)
    }

    private void ShootGhosts() {
        // Has the boss shoot ghosts toward the player.
    }
}

public class Player : MonoBehaviour {

    public float speed;
    private int lives;

    private void OnEatPellet() {
        // Adds to the score
    }

    private void OnEatFruit() {
        // Adds more points to the score
    }

    private void OnEatPowerPellet() {
        // Waka-waka, spoop eating time
    }

    private void OnEatUltraPellet() {
        // Honestly I have no idea what an Ultra pellet should do
    }

    public void Die() {
        // R.I.P. player has died, switch to game over screen.
    }
}

public class GameManager : MonoBehaviour {

    public int score;
    public Transform spawnPoint;

    public void LoadNextLevel() {
        // Loads the next level
    }

    public void RespawnPlayer() {
        // Places the player back at the spawn point
    }

    public void OpenMenu() {
        // Opens the menu when the player presses a button
    }

    public void ReturnToMenu() {
        // Returns to main menu (?)
    }
}

public class Consumable : MonoBehaviour {

    public string itemType;
}

public class BossPellet : Consumable {

    private void OnPelletEaten() {
        // Player can now eat the boss (or at least do some damage)
    }
}