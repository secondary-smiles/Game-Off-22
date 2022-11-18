using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public Animator playerAnimator;
    public Transform orientation;
    public Transform groundCheckPoint;
    public Transform camPos;

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

    [Header("Grapplegunning")]
    public Transform grappleShootPoint;
    public float grappleAimAssist;
    public float maxGrappleRange;
    public float ropeSpring;
    public float damper;
    public float maxPullRangeMultiplier;
    public float minPullRangeMultiplier;
    public float massScale;
    public float grappleLerpSpeedMultiplier;
    
    

    [Header("Jumping")]
    public float jumpStrength = 14f;
    public int extraJumps = 1;
    public Vector3 jumpDirection = Vector3.up;

    [Header("Drag")]
    public float groundDrag = 6f;
    public float airDrag = 2f;

    [Header("Misc")]
    public float gravity = 9.8f;
    public float groundCheckRadius = 0.501f;
    public float maxWallDistance = 1f;
    public float maxSpeed = 140f;

    [NonSerialized] public Vector3 moveDirection;
    [NonSerialized] public Rigidbody playerBody;

    [NonSerialized] public float movementMultiplier = 10f;

    [NonSerialized] public float horizontalMovement;
    [NonSerialized] public float verticalMovement;

    [NonSerialized] public bool wallRunning = false;

    [NonSerialized] public Wall wallData;

    [NonSerialized] public float rawMaxRecordedSpeed = 0f;
    [NonSerialized] public float maxRecordedSpeed = 0f;

    public Slope slopeData => IsGrounded();

    public float forwardsSpeed => Mathf.Abs((playerBody.velocity.z / 1000) * 3600);
    public float rawforwardsSpeed => (playerBody.velocity.z / 1000) * 3600;

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

        playerAnimator.SetBool("OnWall", wallRunning);
        playerAnimator.SetFloat("Speed", forwardsSpeed);
    }

    private void LateUpdate() {
        Vector3 velocity = playerBody.velocity;
        if (forwardsSpeed > maxSpeed) {
            float direction = velocity.z < 0 ? -1 : 1;
            velocity.z = (maxSpeed * 1000 / 3600) * direction;
            playerBody.velocity = velocity;
        }

        if (forwardsSpeed > maxRecordedSpeed) {
            maxRecordedSpeed = forwardsSpeed;
            rawMaxRecordedSpeed = rawforwardsSpeed;
        }
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
            try {
                return hitCollider.gameObject.GetComponent<Tags>().hasTag(tag);
            } catch {
                return false;
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
        gameObject.AddComponent<Walk>().player = this;
        gameObject.AddComponent<Jump>().player = this;
        gameObject.AddComponent<Sprint>().player = this;
        gameObject.AddComponent<WallRun>().player = this;
        gameObject.AddComponent<GrappleSwing>().player = this;
    }
}
