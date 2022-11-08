using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour {
    PlayerController player;

    private float prevZVel;

    // Start is called before the first frame update
    void Start() {
        player = GetComponent<PlayerController>();
        prevZVel = player.playerMoveVelocity.z;
    }


    // Update is called once per frame
    void Update() {
        if (player.onWall.wall && !player.isJumping) {
            // Set params
            player.currentGravity = player.wallGravity * player.movementMultiplier;
            player.moveSpeed = player.wallSpeed;
            player.currentDrag = player.wallDrag;
            player.playerJumpDirection = player.onWall.jumpDirection;
            player.currentJumpHeight = player.wallJumpHeight;

            print(player.onWall.rawNormal);

            // Freeze vertical
            player.playerBody.velocity = new Vector3(player.playerBody.velocity.x, 0, player.playerBody.velocity.z);
        } else {
            // Reset params
            player.currentGravity = player.groundGravity;
            player.playerJumpDirection = Vector3.up;
            player.currentJumpHeight = player.groundJumpHeight;
        }
    }
}
