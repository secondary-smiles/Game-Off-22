using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour {
    PlayerController player;

    bool canCallTrigger = true;
    // Start is called before the first frame update
    void Start() {
        player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update() {
        if (!player.slopeData.grounded && player.wallData.onWall) {
            player.wallRunning = true;
            HandleWallrunOn();
        } else {
            HandleWallrunOff();
            player.wallRunning = false;
        }

        player.playerAnimator.SetBool("OnWall", player.wallData.onWall);
    }

    private void HandleWallrunOn() {
        if (canCallTrigger) {
            if (player.wallData.side == -1) {
                player.playerAnimator.SetTrigger("Wallrun_L");
            } else {
                player.playerAnimator.SetTrigger("Wallrun_R");
            }
            canCallTrigger = false;
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
