using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunFX : MonoBehaviour {
    public PlayerController player;

    FXManager manager;

    private void Start() {
        manager = GetComponent<FXManager>();
    }

    // Update is called once per frame
    void Update() {
        if (player.wallRunning) {
            WallRunFXOn();
        } else {
            WallRunFXOff();
        }
    }

    private void WallRunFXOn() {
        if (player.forwardsSpeed < manager.wallRunActivateSpeed) return;

        manager.currentFov = Mathf.Lerp(manager.currentFov, manager.wallRunFov, manager.timeToWallRunFov * Time.deltaTime);
    }

    private void WallRunFXOff() {
        manager.currentFov = Mathf.Lerp(manager.currentFov, manager.defaultFov, manager.timeToWallRunFov * Time.deltaTime);
    }
}
