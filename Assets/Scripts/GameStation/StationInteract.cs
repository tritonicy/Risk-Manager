using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

public class StationInteract : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    private CameraManagement cameraManagement;
    public static bool isPlayingTetris = false;
    public static Action OnPlayTetris;

    private void Start() {
        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.isTrigger = true;
        cameraManagement = FindObjectOfType<CameraManagement>();
    }
    
    private void Update() {
        if(boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Player"))) {
            if(Input.GetKeyDown(KeyCode.F)) {
                OnPlayTetris?.Invoke();
                if(cameraManagement.FindActiveCamera().transform.position.x >= -10f) {
                    isPlayingTetris = true;
                    cameraManagement.ChangeCamera();
                    cameraManagement.ChangeMinimapCameraState(false);
                    return;

            }
                else{
                isPlayingTetris = false;
                OnPlayTetris?.Invoke();
                cameraManagement.ChangeCamera();
                cameraManagement.ChangeMinimapCameraState(true);
                return;
            }
        }
        }
    }
}
