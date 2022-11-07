using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour {
    PlayerController player;
    // Start is called before the first frame update
    void Start() {
        player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update() {
        if (player.onWall.wall) {
            player.currentGravity = player.wallGravity;
            player.moveSpeed = player.wallSpeed;
            player.currentDrag = player.wallDrag;
            player.playerJumpDirection = player.onWall.jumpDirection;
            player.currentJumpHeight = player.wallJumpHeight;
        } else {
            player.currentGravity = player.groundGravity;
            player.playerJumpDirection = Vector3.up;
            player.currentJumpHeight = player.groundJumpHeight;
        }
    }
}
