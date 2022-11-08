using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunFX : MonoBehaviour {
    public PlayerController player;

    CamFXManager manager;

    float camTilt;

    private void Start() {
        manager = GetComponent<CamFXManager>();
        camTilt = manager.cam.transform.rotation.z;
    }

    // Update is called once per frame
    void Update() {
        if (player.wallRunning) {
            WallRunFXOn();
        } else {
            WallRunFXOff();
        }
        manager.cam.transform.localRotation = Quaternion.Euler(manager.cam.transform.rotation.x, manager.cam.transform.rotation.y, camTilt);
    }

    private void WallRunFXOn() {
        if (player.forwardsSpeed < manager.wallRunActivateSpeed) return;

        manager.currentFov = Mathf.Lerp(manager.currentFov, manager.wallRunFov, manager.timeToWallRunFX * Time.deltaTime);

        camTilt = Mathf.Lerp(camTilt, manager.wallRunTilt * player.wallData.side, manager.timeToWallRunFX * Time.deltaTime);
    }

    private void WallRunFXOff() {
        manager.currentFov = Mathf.Lerp(manager.currentFov, manager.defaultFov, manager.timeToWallRunFX * Time.deltaTime);
        camTilt = Mathf.Lerp(camTilt, 0, manager.timeToWallRunFX * Time.deltaTime);
    }
}
