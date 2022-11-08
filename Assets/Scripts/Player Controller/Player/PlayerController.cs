using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public Transform orientation;
    public Transform groundCheckPoint;

    [Header("Movement")]
    public float currentMovementSpeed = 6f;
    public float groundMovementMultiplier = 10f;
    public float airMovementMultiplier = 0.5f;

    [Header("Walking and Sprinting")]
    public float walkSpeed = 6f;
    public float sprintSpeed = 12f;
    public float timeToSprint = 10f;

    [Header("Jumping")]
    public float jumpStrength = 5f;

    [Header("Drag")]
    public float groundDrag = 6f;
    public float airDrag = 2f;

    [Header("Misc")]
    public float gravity = 9.8f;
    public float groundCheckRadius = 0.501f;
    public float maxWallDistance = 1f;

    [System.NonSerialized] public Vector3 moveDirection;
    [System.NonSerialized] public Rigidbody playerBody;

    [System.NonSerialized] public float movementMultiplier = 10f;

    [System.NonSerialized] public float horizontalMovement;
    [System.NonSerialized] public float verticalMovement;

    public Slope slopeData => IsGrounded();
    public Wall wallData;

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
        if (slopeData.grounded) {
            currentDrag = groundDrag;
            movementMultiplier = groundMovementMultiplier;
        } else {
            currentDrag = airDrag;
            movementMultiplier = airMovementMultiplier;
        }
        Physics.gravity = Vector3.down * gravity;
        transform.rotation = orientation.rotation;
        wallData = new Wall(orientation, maxWallDistance);
        print(wallData.side);
    }

    private void CaptureInput() {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
    }

    private Slope IsGrounded() {
        Slope slope = new Slope(true, true, Vector3.up);
        slope.grounded = CheckTagSphere("Ground");
        if (!slope.grounded) { slope.groundedRaw = CheckSphere(); }

        if (slope.groundedRaw) { slope.normal = GetNormalDirection(Vector3.down); }
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

    private bool CheckSphere() {
        bool returnCheck = false;
        Collider[] hitColliders = Physics.OverlapSphere(groundCheckPoint.position, groundCheckRadius);
        foreach (var hitCollider in hitColliders) {
            if (hitCollider.gameObject.GetComponent<Tags>()) {
                if (!hitCollider.gameObject.GetComponent<Tags>().hasTag("Entity")) {
                    returnCheck = true;
                }
            }
        }
        return returnCheck;
    }

    private Vector3 GetNormalDirection(Vector3 direction) {
        Ray ray = new Ray(groundCheckPoint.position, direction);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        return hit.normal;
    }

    private void _StartupAddComponents() {
        gameObject.AddComponent<Walk>();
        gameObject.AddComponent<Jump>();
        gameObject.AddComponent<Sprint>();
    }
}
