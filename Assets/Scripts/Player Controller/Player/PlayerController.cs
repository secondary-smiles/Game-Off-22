using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {
    public Transform orientation;
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

    private void LateUpdate() {
        gameObject.transform.rotation = orientation.rotation;
    }

    private void FixedUpdate() {
        ApplyGravity();
    }

    void ApplyGravity() {
        if (isGrounded || isJumping) { gravityMultiplier = 1f; return;  }

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
    }
}
