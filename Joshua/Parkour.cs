using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Parkour : MonoBehaviour
{
    Transform chController;
    private bool inside = false;
    [SerializeField]
    private float heightFactor = 8.0f;
    private KeyCode shift = KeyCode.LeftShift;
    private string parkourObj;
    [SerializeField]
    private float speed = 14.0f;
    [SerializeField]
    private float slideCooldown = 1.5f;
    [SerializeField]
    private float slideCooldownTime = 1f;


    void Start(){

    }


    //check for collision with gameobject with tag
    void OnTriggerEnter(Collider Col){
        if (Col.gameObject.tag == "Ladder")
        {
            parkourObj = "Ladder";
            inside = !inside;
        }

        if (Col.gameObject.tag == "ClimbWall")
        {
            parkourObj = "ClimbWall";
            inside = !inside;
        }
    }


    //check for leaving collision with gameobject with tag
    void OnTriggerExit(Collider Col){
        if (Col.gameObject.tag == "Ladder")
        {

            inside = !inside;
        }

        if (Col.gameObject.tag == "ClimbWall")
        {

            inside = !inside;
        }
    }


    void Climbing(){
        //check for gameobject with tag and key input, if true climb upwards
        if (inside == true && parkourObj == "Ladder" && Input.GetKey("w"))
        {
            this.transform.position += Vector3.up;
        }

        if (inside == true && parkourObj == "RunWall" && Input.GetKey("w") || inside == true && parkourObj == "RunWall" && Input.GetKey("D") || inside == true && parkourObj == "RunWall" && Input.GetKey("A"))
        {
            this.transform.position += Vector3.forward;
            this.transform.position += Vector3.up/heightFactor; 
        }

        if (inside == true && parkourObj == "ClimbWall" && Input.GetKey("w") && Input.GetKey(shift))
        {
            this.transform.position += Vector3.up / heightFactor;
        }
    }


    void Sliding()
    {
        //check for key input and cooldown float
        if( Input.GetKey("w") && Input.GetKey(KeyCode.LeftControl) && slideCooldown > 0)
        {
            // rotate player body, move forwards and lock camera at angle
            float rotY = transform.eulerAngles.y;
            transform.rotation = Quaternion.Euler(-90, rotY * 1, 0);
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            GetComponentInChildren<Camera>().transform.rotation = Quaternion.Euler(-15, rotY * 1, 0);
            slideCooldown -= Time.deltaTime;
        }
        else if(slideCooldown < -1)
        {
            slideCooldown = slideCooldownTime;
        }
        else
        {
            slideCooldown -= Time.deltaTime;
        }
    }


    void Update()
    {
        Climbing();
        Sliding();
        Menu();
    }


    void Menu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }


}