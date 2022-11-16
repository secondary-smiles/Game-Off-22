using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour {
    public PlayerController player;

    private void Update() {
        player.moveDirection = player.orientation.forward * player.verticalMovement + player.orientation.right * player.horizontalMovement;
    }

    private void FixedUpdate() {
        Vector3 velocity = player.slopeData.MoveDirection(player.movementMultiplier * player.currentMovementSpeed * player.moveDirection.normalized);
        player.playerBody.AddForce(velocity, ForceMode.Acceleration);
    }
}
