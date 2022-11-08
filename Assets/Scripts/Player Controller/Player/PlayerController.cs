using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public Transform orientation;
    public Transform groundCheckPoint;

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
    public float groundCheckRadius = 0.501f;

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
        slope.grounded = CheckTagSphere("Ground");
        slope.groundedRaw = slope.grounded;

        return slope;
    }

    private bool CheckTagSphere(string tag) {
        Collider[] hitColliders = Physics.OverlapSphere(groundCheckPoint.position, groundCheckRadius);
        foreach (var hitCollider in hitColliders) {
            if (hitCollider.gameObject.GetComponent<Tags>()) {
                if (hitCollider.gameObject.GetComponent<Tags>().hasTag(tag)) {
                    return true;
                }
            }
        }
        return false;
    }

    private void _StartupAddComponents() {
        gameObject.AddComponent<Walk>();
        gameObject.AddComponent<Jump>();
    }
}
