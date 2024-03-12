using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] DoorScript otherDoor;
    private CameraManagement cameraManagement;


    private void Start() {
        cameraManagement = FindObjectOfType<CameraManagement>();
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "player") {
            if (cameraManagement.FindActiveCamera().transform.position.x > 5f) {
                other.transform.position = otherDoor.transform.position + Vector3.left;
                cameraManagement.FindActiveCamera().transform.position = new Vector3(0,0,-10);
            }
            else {
                other.transform.position = otherDoor.transform.position + Vector3.right;
                cameraManagement.FindActiveCamera().transform.position = new Vector3(17.5f,0,-10);

            }

        }
    }
}
