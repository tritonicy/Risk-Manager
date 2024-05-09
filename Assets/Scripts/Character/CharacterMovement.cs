using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;
public class CharacterMovement : MonoBehaviour
{
    public PlayerInputSystem playerInputSystem;
    private Rigidbody2D rb;
    [SerializeField] float walkSpeed = 2f;
    private Vector2 moveDirection;
    private CameraManagement cameraManagement;
    [SerializeField] Animator animator;



    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        cameraManagement = FindObjectOfType<CameraManagement>();
    
    }
    private void Awake() {
        playerInputSystem = new PlayerInputSystem();
    }

    private void FixedUpdate() {
        if (!StationInteract.isPlayingTetris)
        {
            if (cameraManagement.FindActiveCamera().transform.position.x <= -10f)
            {
                return;
            }
            if (cameraManagement.FindActiveCamera().transform.position.x < 5f)
            {
                rb.velocity = new Vector2(moveDirection.x * walkSpeed, rb.velocity.y);
                if(rb.velocity.x != 0)  {
                    animator.SetBool("isRunning", true);
                }
                else{
                    animator.SetBool("isRunning", false);
                }
                if(rb.velocity.x < 0 && this.transform.localScale.x > 0) {
                    FlipSprite();
                }
                else if(rb.velocity.x > 0 && this.transform.localScale.x < 0) {
                    FlipSprite();
                }
            }
        }

    }

    private void OnEnable() {
        playerInputSystem.Player.Enable();
        StationInteract.OnPlayTetris += HandleCharacterMovement;
    }

    private void HandleCharacterMovement()
    {
        rb.velocity = new Vector2(0f,0f);
    }

    private void OnDisable() {
        playerInputSystem.Player.Disable();
    }

    public void OnMove(InputValue value) {
        moveDirection = value.Get<Vector2>();
    }
    public void FlipSprite() {
        Vector3 localScale = this.transform.localScale;
        localScale.x = - localScale.x;
        this.transform.localScale = localScale;
    }

}
