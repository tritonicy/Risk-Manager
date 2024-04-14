using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StationInteract : MonoBehaviour
{
    private PolygonCollider2D polygonCollider2D;
    private CameraManagement cameraManagement;
    private void Start() {
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        polygonCollider2D.isTrigger = true;
        cameraManagement = FindObjectOfType<CameraManagement>();
    }
    
    private void Update() {
        if(polygonCollider2D.IsTouchingLayers(LayerMask.GetMask("Player"))) {
            if(Input.GetKeyDown(KeyCode.F)) {
                if(cameraManagement.FindActiveCamera().transform.position.x >= -10f) {
                    cameraManagement.ChangeCamera();
                    cameraManagement.ChangeMinimapCameraState(false);
                    return;

            }
            else{
                cameraManagement.ChangeCamera();
                cameraManagement.ChangeMinimapCameraState(true);
                return;
            }
        }
        }
    }
}
