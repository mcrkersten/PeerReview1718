using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour {

    public int damage;
    public int bulletSpeed;
    public float reloadTime;
    public int ammo;
    public int curAmmo;

    private void Reload() {
        if (curAmmo == 0 || Input.GetKeyDown(KeyCode.R)) {
            StartCoroutine(Reloading());
        }
    }

    IEnumerator Reloading() {
        yield return new WaitForSeconds(reloadTime);
        curAmmo = ammo;
    }

    //Bij een soldaat op schoot?
    public virtual void Schoot() {
        curAmmo--;
        //raycast blahblah
    }
}
