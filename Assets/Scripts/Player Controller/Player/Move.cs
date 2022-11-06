using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    PlayerController player;
    // Start is called before the first frame update
    void Start() {
        player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update() {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = player.orientation.forward * moveZ + player.orientation.right * moveX;
        player.playerMoveVelocity = moveDirection;
    }

    private void FixedUpdate() {
        float airSpeedMultiplier = player.isGrounded ? 1 : player.airSpeed;
        player.playerBody.AddForce(player.movementMultiplier * player.moveSpeed * airSpeedMultiplier * player.playerMoveVelocity.normalized, ForceMode.Acceleration);
    }
}
