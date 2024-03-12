using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CameraManagement : MonoBehaviour
{
    [SerializeField] private List<Camera> cams;
    [SerializeField] private RawImage minimapIcon;

    public Camera FindActiveCamera() {
        for(int i = 0 ; i < cams.Count; i++) {
            if(cams[i].enabled) {
                return cams[i];
            }
        }
        return null;
    }
    public Camera FindDeactiveCamera() {
        for(int i = 0 ; i < cams.Count; i++) {
            if(!cams[i].enabled) {
                return cams[i];
            }
        }
        return null;
    }
    public void ChangeCamera() {
        Camera activeCam = FindActiveCamera();
        Camera deactiveCamera = FindDeactiveCamera();

        activeCam.enabled = false;
        deactiveCamera.enabled = true;
    }

    public void ChangeMinimapCameraState(bool val) {
        if(val== true) {
            minimapIcon.enabled = true;
        }
        else{
            minimapIcon.enabled = false;
        }
    }
}