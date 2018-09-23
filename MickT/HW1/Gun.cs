using UnityEngine;
using VRTK;


public class Gun : MonoBehaviour {

    private float damage = 10f;
     public float Damage {
        get { return damage; }
        set { damage = value; }
    }

    private float range = 100f;
     public float Range {
        get { return range; }
        set { range = value; }
    }

    private float fireRate = 15f;
     public float FireRate {
        get { return fireRate; }
        set { fireRate = value; }
    }

    private float impactForce = 30f;
     public float ImpactForce{
        get { return impactForce; }
        set { impactForce = value; }
    }

    private bool holding = false;
     public bool Holding {
        get { return holding; }
        set { holding = value; }

    }

    public GameObject rightController;
    public GameObject leftController;
    public GameObject grabLeft;
    public GameObject grabRight;
    public GameObject gunEnd;
    public GameObject impactEffect;
    public Animator anim;
    public AudioClip gunFire;
    public ParticleSystem muzzleFlash;
    public ParticleSystem bulletShell;

    private AudioSource AS;
    private VRTK_ControllerEvents RCE;
    private VRTK_InteractGrab RCG;
    private VRTK_ControllerEvents LCE;
    private VRTK_InteractGrab LCG;
    private float nextTimeToFire = 0f;

/// <summary>
/// VRTK is a plugin for vr in unity, this checks if the person is holding the gun in vr, if he is holding the gun only then you can shoot
/// </summary>
    private void Awake() {
        RCE = rightController.GetComponent<VRTK_ControllerEvents>();
        LCE = leftController.GetComponent<VRTK_ControllerEvents>();
        RCG = rightController.GetComponent<VRTK_InteractGrab>();
        LCG = leftController.GetComponent<VRTK_InteractGrab>();
    }

    private void Start() {
        anim = GetComponent<Animator>();
        AS = GetComponent<AudioSource>();
    }

    private void CheckHolding() {
        grabRight = RCG.GetGrabbedObject();
        grabLeft = LCG.GetGrabbedObject();

        if (grabRight == gameObject || grabLeft == gameObject) holding = true;

        else holding = false;
        
    }

    private void Update() {
        CheckHolding();
        if (holding) {
            if ((RCE.triggerPressed || LCE.triggerPressed) && Time.time >= nextTimeToFire) {
                AS.PlayOneShot(GunFire);
                anim.Play("fire");
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }
    }

    private void Shoot() {
        muzzleFlash.Play();
        bulletShell.Play();

        RaycastHit hit;
        if (Physics.Raycast(GunEnd.transform.position, GunEnd.transform.forward, out hit, range)) {
            Debug.Log(hit.transform.name);

            Enemy target = hit.transform.GetComponent<Enemy>();
            if (target != null) target.TakeDamage(damage);
            

            if (hit.rigidbody != null)
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }

}
