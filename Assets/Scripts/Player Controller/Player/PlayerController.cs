using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {
    public Transform orientation;
    public Transform groundCheckPoint;

    [Header("Ground Movement")]
    public float walkSpeed = 12f;
    public float sprintSpeed = 25f;
    public float timeToSprint = 40f;
    public float groundDrag = 12f;

    [Header("Air Movement")]
    public float airSpeed = 0.5f;
    public float airDrag = 5f;

    [Header("Wall Movement")]
    public float wallSpeed = 16f;
    public float wallDrag = 2f;

    [Header("Gravity Params")]
    public float jumpHeight = 12f;
    public float currentGravity = 9.8f;
    public float groundGravity = 9.8f;
    public float wallGravity = 3.2f;

    [Header("Misc")]
    public float movementMultiplier = 10f;

    [Header("ONLY TOUCH IF YOU KNOW WHAT YOU'RE DOING")]
    [SerializeField] private float groundCheckRadius = 0.501f;

    [System.NonSerialized] public Rigidbody playerBody;

    [System.NonSerialized] public Vector3 playerMoveVelocity;

    [System.NonSerialized] public Vector3 playerJumpVelocity;

    [System.NonSerialized] public bool isJumping;

    [System.NonSerialized] public float moveSpeed;

    public Slope isGrounded => PopulateSlope();

    public Wall onWall => PopulateWall();

    private float _currentDrag;
    public float currentDrag {
        get { return _currentDrag; }
        set {
            _currentDrag = value;
            playerBody.drag = value;
        }
    }

    private float _currentMass;
    public float currentMass {
        get { return _currentMass; }
        set {
            _currentMass = value;
            playerBody.mass = value;
        }
    }

    private float gravityMultiplier = 1f;

    // Start is called before the first frame update
    void Start() {
        playerBody = GetComponent<Rigidbody>();
        playerBody.freezeRotation = true;
        playerBody.useGravity = false;

        _startupAddComponents();
    }

    private void Update() {
        gameObject.transform.rotation = orientation.rotation;
    }

    private void FixedUpdate() {
        ApplyGravity();
    }

    void ApplyGravity() {
        if ((isGrounded.groundedRaw && !isGrounded.onSlope && onWall.side == 0) || isJumping) { gravityMultiplier = 1f; return; }
        if (isGrounded.onSlope) { gravityMultiplier = 1f; }
        Vector3 gravityVelocity = (currentGravity - 1) * gravityMultiplier * playerBody.mass * Physics.gravity;
        gravityMultiplier += (0.1f * movementMultiplier) * Time.fixedDeltaTime;
        gravityMultiplier = Mathf.Clamp(gravityMultiplier, 1, 10);
        playerBody.AddForce(gravityVelocity);
    }

    private Slope PopulateSlope() {
        Slope slope = new Slope(true, true, Vector3.zero);
        slope.grounded = CheckTagSphere("Ground");
        slope.groundedRaw = CheckIsGroundedSphereRaw();
        if (slope.grounded) {
            slope.normal = GetNormal(groundCheckPoint.transform.position);
        } else {
            slope.normal = Vector3.up;
        }
        return slope;
    }

    private Wall PopulateWall() {
        Wall wall = new Wall(0);
        if (isGrounded.grounded || !CheckTagSphere("Wall")) return wall;
        wall.side = wall.DetectWall(orientation.transform);
        return wall;
    }

    private Vector3 GetNormal(Vector3 start) {
        RaycastHit hit;
        Physics.Raycast(start, Vector3.down, out hit);
        return hit.normal;
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

    private bool CheckIsGroundedSphereRaw() {
        bool returnVal = false;
        Collider[] hitColliders = Physics.OverlapSphere(groundCheckPoint.position, groundCheckRadius);
        foreach (var hitCollider in hitColliders) {
            if (hitCollider.gameObject.GetComponent<Tags>()) {
                if (!hitCollider.gameObject.GetComponent<Tags>().hasTag("Player")) {
                    returnVal = true;
                }
            }
        }
        return returnVal;
    }

    void _startupAddComponents() {
        gameObject.AddComponent<Move>();
        gameObject.AddComponent<Jump>();
        gameObject.AddComponent<Sprint>();
        gameObject.AddComponent<WallRun>();
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
    }
}
