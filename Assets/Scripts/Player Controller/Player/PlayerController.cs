using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float groundSpeed = 12f;
    public float airSpeed = 0.2f;
    public float gravity = 9.8f;
    public float groundDrag = 12f;
    public float airDrag = 2f;
    public float jump = 12f;

    public float movementMultiplier = 20f;

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


    public bool isGrounded => CheckIsGrounded();
    public bool isGroundedRaw => CheckIsGroundedRaw();

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


    // Start is called before the first frame update
    void Start() {
        playerBody = GetComponent<Rigidbody>();
        playerBody.freezeRotation = true;
        playerBody.useGravity = false;

        _startupAddComponents();
    }

    private void FixedUpdate() {
        ApplyGravity();
    }

    void ApplyGravity() {
        if (isGroundedRaw || isJumping) return;
        //Vector3 gravityVelocity = gravity * movementMultiplier * new Vector3(0, -1.0f, 0);
        Vector3 gravityVelocity = Physics.gravity * (gravity - 1) * playerBody.mass;

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

    private bool CheckIsGroundedRaw() {
        CapsuleCollider collider = GetComponentInChildren<CapsuleCollider>();
        Ray ray = new Ray(collider.transform.position, Vector3.down);
        float distance = playerHeight / 2 + 0.01f;
        bool check = Physics.Raycast(ray, distance);

        return check;
    }

    void _startupAddComponents() {
        gameObject.AddComponent<Move>();
        gameObject.AddComponent<Jump>();
    }
}
