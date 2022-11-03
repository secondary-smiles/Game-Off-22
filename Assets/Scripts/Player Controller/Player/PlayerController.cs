using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    public Rigidbody playerBody;

    public float speed = 12f;

    [System.NonSerialized]
    public Vector3 playerVelocity;

    // Start is called before the first frame update
    void Start() {
        playerBody.freezeRotation = true;
    }

    // Update is called once per frame
    void Update() {

    }

    private void FixedUpdate() {
        MovePlayer();
    }

    void MovePlayer() {
        playerBody.AddForce(playerVelocity.normalized * speed, ForceMode.Acceleration);
    }
}
