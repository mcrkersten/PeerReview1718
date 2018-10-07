using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float mouseSensitivity;

    private Vector3 targetRotCam;
    private Vector3 targetRotPlayer;

    private float mouseX;
    private float mouseY;

    private float rotAmountX;
    private float rotAmountY;

    private float xAxisClamp = 0;


	// Mouse input in the update function as well as the rotatecamera function
	private void Update () {
        Cursor.lockState = CursorLockMode.Locked;
        if (!GameObject.Find("Player").GetComponent<PlayerController>().enabled){
            mouseX = Input.GetAxis("Mouse X");
            transform.rotation = Quaternion.Euler(new Vector3(0, targetRotCam.y, targetRotCam.z));
        } else if (GameObject.Find("Player").GetComponent<PlayerController>().enabled){
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");
            RotateCamera();
        }
	}


    private void RotateCamera(){
        rotAmountX = mouseX * mouseSensitivity;
        rotAmountY = mouseY * mouseSensitivity;
        targetRotCam = transform.rotation.eulerAngles;
        targetRotPlayer = player.rotation.eulerAngles;

        xAxisClamp -= rotAmountY;

        targetRotCam.x -= rotAmountY;
        targetRotCam.z = 0f;
        targetRotPlayer.y += rotAmountX;

        //The cam must stay between the two max rotations on the x axis. These lines fixes that
        if (GameObject.Find("Player").GetComponent<ClimbUp>().camReset) xAxisClamp = 0;
        if (xAxisClamp > 90 ){
            xAxisClamp = 90;
            targetRotCam.x = 90;
        } else if (xAxisClamp < -90){
            xAxisClamp = -90;
            targetRotCam.x = 270;
        }

        transform.rotation = Quaternion.Euler(targetRotCam);
        player.rotation = Quaternion.Euler(targetRotPlayer);
    }
}
