using UnityEngine;
using VRTK;


public class Gun : MonoBehaviour
{

    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;

    private AudioSource AS;
    public AudioClip GunFire;

    public bool Holding = false;

    public GameObject RightController;
    private VRTK_ControllerEvents RCE;
    private VRTK_InteractGrab RCG;
    public GameObject LeftController;
    private VRTK_ControllerEvents LCE;
    private VRTK_InteractGrab LCG;

    public GameObject GrabLeft;
    public GameObject GrabRight;

    public Animator anim;
    public GameObject GunEnd;
    public ParticleSystem MuzzleFlash;
    public ParticleSystem BulletShell;
    public GameObject ImpactEffect;

    private float nextTimeToFire = 0f;

    void Awake()
    {
        RCE = RightController.GetComponent<VRTK_ControllerEvents>();
        LCE = LeftController.GetComponent<VRTK_ControllerEvents>();
        RCG = RightController.GetComponent<VRTK_InteractGrab>();
        LCG = LeftController.GetComponent<VRTK_InteractGrab>();
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        AS = GetComponent<AudioSource>();
    }

    void CheckHolding()
    {
        GrabRight = RCG.GetGrabbedObject();
        GrabLeft = LCG.GetGrabbedObject();

        if (GrabRight == gameObject || GrabLeft == gameObject)
        {
            Holding = true;
        }
        else
        {
            Holding = false;
        }
    }

    void Update()
    {
        CheckHolding();
        if (Holding)
        {
            if ((RCE.triggerPressed || LCE.triggerPressed) && Time.time >= nextTimeToFire)
            {
                AS.PlayOneShot(GunFire);
                anim.Play("fire");
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        MuzzleFlash.Play();
        BulletShell.Play();

        RaycastHit hit;
        if (Physics.Raycast(GunEnd.transform.position, GunEnd.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Enemy target = hit.transform.GetComponent<Enemy>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactGO = Instantiate(ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }

}
