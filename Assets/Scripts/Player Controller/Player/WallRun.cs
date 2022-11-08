using System;
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
        if (!player.slopeData.grounded && player.wallData.onWall) {
            player.wallRunning = true;
            HandleWallrunOn();
        } else {
            HandleWallrunOff();
            player.wallRunning = false;
        }
    }

    private void HandleWallrunOn() {
        player.useGravity = false;
        
        player.playerBody.AddForce(Vector3.down * player.wallRunGravity, ForceMode.Force);
        player.jumpDirection = player.wallData.wallNormal + transform.up;
    }

    private void HandleWallrunOff() {
        player.useGravity = true;
        player.jumpDirection = transform.up;
    }
}
