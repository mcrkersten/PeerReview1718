using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapons {
    public GameObject projectile;
    public int projectileAmount;

    public override void Schoot() {
        curAmmo -= projectileAmount;
    }
}
