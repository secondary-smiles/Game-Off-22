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

    [System.NonSerialized]
    public bool isGrounded;

    // Start is called before the first frame update
    void Start() {
        playerBody = GetComponent<Rigidbody>();
        playerBody.freezeRotation = true;
        playerBody.useGravity = false;

        _startupAddComponents();
    }

    private void FixedUpdate() {
        MovePlayer();
        ApplyGravity();
    }

    void MovePlayer() {
        playerBody.AddForce(movementMultiplier * speed * playerVelocity.normalized, ForceMode.Acceleration);
    }

    void ApplyGravity() {
        Vector3 gravityVelocity = new Vector3(0, -1.0f, 0) * playerBody.mass * gravity;

        playerBody.AddForce(gravityVelocity);
    }

    void _startupAddComponents() {
        gameObject.AddComponent<Move>();
    }
}
