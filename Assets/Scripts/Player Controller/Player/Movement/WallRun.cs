using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour {
    public PlayerController player;

    bool canCallTrigger = true;
    float prevSide = 0;

    // Update is called once per frame
    void Update() {
        if (!player.slopeData.grounded && player.wallData.onWall) {
            player.wallRunning = true;
            HandleWallrunOn();
        } else {
            HandleWallrunOff();
            player.wallRunning = false;
        }

    }

    private void HandleWallrunOn() {
        if (canCallTrigger || player.wallData.side != prevSide) {
            if (player.wallData.side == -1) {
                player.playerAnimator.SetTrigger("Wallrun_L");
            } else {
                player.playerAnimator.SetTrigger("Wallrun_R");
            }
            canCallTrigger = false;
            prevSide = player.wallData.side;
        }
        player.useGravity = false;

        player.playerBody.AddForce(Vector3.down * player.wallRunGravity, ForceMode.Force);
        player.jumpDirection = player.wallData.wallNormal + transform.up;
    }

    private void HandleWallrunOff() {
        player.useGravity = true;
        player.jumpDirection = transform.up;
        canCallTrigger = true;
    }
}
