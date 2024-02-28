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
    [SerializeField] float jumpSpeed = 40f;
    private Vector2 moveDirection;
    private bool isTouchingFloor = false;
    private readonly float gravity = 9.84f;
    [SerializeField] float newGravity = 15f;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Awake() {
        playerInputSystem = new PlayerInputSystem();
    }

    private void FixedUpdate() {
        rb.velocity = new Vector2(moveDirection.x * walkSpeed,0f);

        if (isTouchingFloor) {
            rb.gravityScale = gravity;
            rb.AddForce(new Vector2(0f, moveDirection.y * jumpSpeed),ForceMode2D.Impulse);
            isTouchingFloor = false;
        }
        else {
            rb.gravityScale = newGravity;
        }
    }

    private void OnEnable() {
        playerInputSystem.Player.Enable();
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
