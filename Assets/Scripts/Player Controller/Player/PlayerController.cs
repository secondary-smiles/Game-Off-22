using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 12f;
    public float drag = 12f;
    public float gravity = 9.8f;

    public float movementMultiplier = 20f;

    [System.NonSerialized]
    public Rigidbody playerBody;

    [System.NonSerialized]
    public Vector3 playerVelocity;

    [SerializeField]
    private float playerHeight;

    private bool grounded;

    public bool isGrounded {
        get { return CheckIsGrounded(); }
        set { grounded = value; }
    }


    // Start is called before the first frame update
    void Start() {
        playerBody = GetComponent<Rigidbody>();
        playerBody.freezeRotation = true;
        playerBody.useGravity = false;

        _startupAddComponents();
    }

    private void Update() {
        Debug.Log(isGrounded);
    }

    private void FixedUpdate() {
        MovePlayer();
        ApplyGravity();
    }


    void MovePlayer() {
        playerBody.AddForce(movementMultiplier * speed * playerVelocity.normalized, ForceMode.Acceleration);
    }

    void ApplyGravity() {
        Vector3 gravityVelocity = gravity * movementMultiplier * playerBody.mass * new Vector3(0, -1.0f, 0);

        playerBody.AddForce(gravityVelocity);
    }

    bool CheckIsGrounded() {
        CapsuleCollider collider = GetComponentInChildren<CapsuleCollider>();
        bool check = Physics.Raycast(collider.transform.position, Vector3.down, playerHeight/2 + 0.01f);
        return check;
    }

    void _startupAddComponents() {
        gameObject.AddComponent<Move>();
    }
}
