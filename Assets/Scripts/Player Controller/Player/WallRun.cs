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
        print(player.onWall.wall);
        if (player.onWall.wall) {
            player.currentGravity = player.wallGravity;
            player.moveSpeed = player.wallSpeed;
            player.currentDrag = player.wallDrag;
            player.playerJumpDirection = player.onWall.jumpDirection;
            player.currentJumpHeight = player.wallJumpHeight;
            player.playerBody.velocity = new Vector3(player.playerBody.velocity.x, 0, player.playerBody.velocity.z);
        } else {
            player.currentGravity = player.groundGravity;
            player.playerJumpDirection = Vector3.up;
            player.currentJumpHeight = player.groundJumpHeight;
        }
    }
}
