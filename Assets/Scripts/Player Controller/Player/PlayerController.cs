using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

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

    [Header("Wallrunning")]
    public float wallRunGravity;

    [Header("Jumping")]
    public float jumpStrength = 17f;
    public int extraJumps = 1;
    public Vector3 jumpDirection = Vector3.up;

    [Header("Drag")]
    public float groundDrag = 6f;
    public float airDrag = 2f;

    [Header("Misc")]
    public float gravity = 9.8f;
    public float groundCheckRadius = 0.501f;
    public float maxWallDistance = 1f;
    public float maxSpeed = 110f;

    [System.NonSerialized] public Vector3 moveDirection;
    [System.NonSerialized] public Rigidbody playerBody;

    [System.NonSerialized] public float movementMultiplier = 10f;

    [System.NonSerialized] public float horizontalMovement;
    [System.NonSerialized] public float verticalMovement;

    [System.NonSerialized] public bool wallRunning = false;

    [System.NonSerialized] public Wall wallData;

    public Slope slopeData => IsGrounded();

    public float forwardsSpeed => Mathf.Abs((playerBody.velocity.z / 1000) * 3600);
    public float forwardsSpeedRaw => (playerBody.velocity.z / 1000) * 3600;

    private float _currentDrag;
    public float currentDrag {
        get => _currentDrag;
        set {
            _currentDrag = value;
            playerBody.drag = value;
        }
    }

    private bool _useGravity;
    public bool useGravity {
        get => _useGravity;
        set {
            _useGravity = value;
            playerBody.useGravity = value;
        }
    }

    private void Start() {
        _StartupAddComponents();
        playerBody = GetComponent<Rigidbody>();
        playerBody.freezeRotation = true;
        playerBody.velocity = Vector3.zero;
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
    }

    private void LateUpdate() {
        Vector3 bodyVelocity = playerBody.velocity;
        if (bodyVelocity.z < 0) {
            bodyVelocity.z = Mathf.Clamp(forwardsSpeedRaw, -maxSpeed, 0);
        } else if (bodyVelocity.z > 0) {
            bodyVelocity.z = Mathf.Clamp(forwardsSpeed, 0, maxSpeed);
        }
        print(bodyVelocity);
        playerBody.velocity = bodyVelocity;
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
        gameObject.AddComponent<WallRun>();
    }
}
