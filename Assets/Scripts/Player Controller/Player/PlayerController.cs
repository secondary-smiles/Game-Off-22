using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {
    public Transform orientation;
    [Header("Ground Movement")]
    public float walkSpeed = 12f;
    public float sprintSpeed = 20f;
    public float groundDrag = 12f;

    [Header("Air Movement")]
    public float airSpeed = 0.2f;
    public float airDrag = 2f;

    [Header("Gravity Params")]
    public float jumpHeight = 12f;
    public float gravity = 9.8f;

    [Header("Misc")]
    public float movementMultiplier = 20f;

    [Header("ONLY TOUCH IF YOU KNOW WHAT YOU'RE DOING")]
    [SerializeField]
    private float playerHeight;


    [System.NonSerialized]
    public Rigidbody playerBody;

    [System.NonSerialized]
    public Vector3 playerMoveVelocity;

    [System.NonSerialized]
    public Vector3 playerJumpVelocity;

    [System.NonSerialized]
    public bool isJumping;

    [System.NonSerialized]
    public float moveSpeed;


    public bool isGrounded => CheckIsGrounded();

    private float _currentDrag;
    public float playerDrag {
        get { return _currentDrag; }
        set {
            _currentDrag = value;
            playerBody.drag = value;
        }
    }

    private float _currentMass;
    public float playerMass {
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
        if (isGrounded || isJumping) { gravityMultiplier = 1f; return; }

        Vector3 gravityVelocity = (gravity - 1) * gravityMultiplier * playerBody.mass * Physics.gravity;
        gravityMultiplier += (0.1f * movementMultiplier) * Time.fixedDeltaTime;
        gravityMultiplier = Mathf.Clamp(gravityMultiplier, 1, 10);
        playerBody.AddForce(gravityVelocity);
    }

    private bool CheckIsGrounded() {
        CapsuleCollider collider = GetComponentInChildren<CapsuleCollider>();
        Ray ray = new Ray(collider.transform.position, Vector3.down);
        RaycastHit hit;
        float distance = playerHeight / 2 + 0.01f;
        bool check = Physics.Raycast(ray, out hit, distance);
        if (check) {
            try {
                return hit.collider.gameObject.GetComponent<Tags>().hasTag("Ground");
            } catch (System.Exception) {
                return false;
            }
        }
        return false;
    }

    void _startupAddComponents() {
        gameObject.AddComponent<Move>();
        gameObject.AddComponent<Jump>();
        gameObject.AddComponent<Sprint>();
    }
}
