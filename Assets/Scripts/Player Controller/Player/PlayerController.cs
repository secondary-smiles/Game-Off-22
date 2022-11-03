using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public Rigidbody playerBody;

    public float speed = 12f;
    public float drag = 6f;

    public float movementMultiplier = 10f;

    [System.NonSerialized]
    public Vector3 playerVelocity;

    // Start is called before the first frame update
    void Start() {
        playerBody.freezeRotation = true;

        StartupAddComponents();
    }

    // Update is called once per frame
    void Update() {

    }

    private void FixedUpdate() {
        MovePlayer();
    }

    void MovePlayer() {
        playerBody.AddForce(playerVelocity.normalized * speed * movementMultiplier, ForceMode.Acceleration);
    }

    void StartupAddComponents() {
        gameObject.AddComponent<Move>();
    }
}
