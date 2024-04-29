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
    [SerializeField] float jumpSpeed = 2f;
    private Vector2 moveDirection;
    private bool isTouchingFloor = false;
    [SerializeField] float leftRoomGravity = 1f;
    private CameraManagement cameraManagement;



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
                rb.gravityScale = leftRoomGravity;
                rb.velocity = new Vector2(moveDirection.x * walkSpeed, rb.velocity.y);
                if (isTouchingFloor)
                {
                    rb.velocity = new Vector2(rb.velocity.x, moveDirection.y * jumpSpeed);
                }
            }
            else
            {
                rb.gravityScale = 0f;
                rb.velocity = new Vector2(moveDirection.x * walkSpeed, moveDirection.y * walkSpeed);
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

    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.tag == "floor") {
            isTouchingFloor = true;
        }
    }
    
    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag == "floor") {
            isTouchingFloor = false;
        }
    }

}
