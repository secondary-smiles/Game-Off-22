using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public Transform orientation;

    [Header("Movement")]
    public float movementSpeed = 6f;
    public float groundMovementMultiplier = 10f;
    public float airMovementMultiplier = 0.5f;

    [Header("Jumping")]
    public float jumpStrength = 5f;

    [Header("Drag")]
    public float groundDrag = 6f;
    public float airDrag = 2f;

    [Header("Misc")]
    public float gravity = 9.8f;

    [System.NonSerialized] public Vector3 moveDirection;
    [System.NonSerialized] public Rigidbody playerBody;

    [System.NonSerialized] public float movementMultiplier = 10f;

    [System.NonSerialized] public float horizontalMovement;
    [System.NonSerialized] public float verticalMovement;

    public Slope isGrounded => IsGrounded();


    private float _currentDrag;
    public float currentDrag {
        get => _currentDrag;
        set {
            _currentDrag = value;
            playerBody.drag = value;
        }
    }

    private void Start() {
        _StartupAddComponents();
        playerBody = GetComponent<Rigidbody>();
        playerBody.freezeRotation = true;

    }


    private void Update() {
        CaptureInput();
        if (isGrounded.grounded) {
            currentDrag = groundDrag;
            movementMultiplier = groundMovementMultiplier;
        }
        Physics.gravity = Vector3.down * gravity;
        transform.rotation = orientation.rotation;
    }

    private void CaptureInput() {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
    }

    private Slope IsGrounded() {
        Slope slope = new Slope(true, true, Vector3.up);
        slope.grounded = CheckIsGroundedTag("Ground");
        slope.groundedRaw = slope.grounded;

        return slope;
    }

    private bool CheckIsGroundedTag(string tag) {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        bool check = Physics.Raycast(ray, out hit, 2 / 2 + 0.01f);

        if (check) {
            try {
                if (hit.collider.gameObject.GetComponent<Tags>().hasTag("Ground")) {
                    return true;
                }
            } catch (Exception) {
                return false;
            }
        }
        return false;
    }

    private void _StartupAddComponents() {
        gameObject.AddComponent<Walk>();
        gameObject.AddComponent<Jump>();
    }
}
