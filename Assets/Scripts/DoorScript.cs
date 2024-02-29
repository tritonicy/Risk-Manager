using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] DoorScript otherDoor;

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "player") {
            if (Camera.main.transform.position.x > 5f) {
                other.transform.position = otherDoor.transform.position + Vector3.left;
                Camera.main.transform.position = new Vector3(0,0,-10);
            }
            else {
                other.transform.position = otherDoor.transform.position + Vector3.right;
                Camera.main.transform.position = new Vector3(10,0,-10);

            }

        }
    }
}
