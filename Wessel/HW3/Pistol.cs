using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapons {
    public GameObject projectile;
    public int projectileAmount;

    public override void Schoot() {
        curAmmo -= projectileAmount;
    }
}
