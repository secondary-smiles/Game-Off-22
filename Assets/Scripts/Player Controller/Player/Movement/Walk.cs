using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour {
    PlayerController player;
    // Start is called before the first frame update
    void Start() {
        player = GetComponent<PlayerController>();
    }

    private void Update() {
        player.moveDirection = player.orientation.forward * player.verticalMovement + player.orientation.right * player.horizontalMovement;
    }

    private void FixedUpdate() {
        Vector3 velocity = player.slopeData.MoveDirection(player.movementMultiplier * player.currentMovementSpeed * player.moveDirection.normalized);
        player.playerBody.AddForce(velocity, ForceMode.Acceleration);
    }
}
