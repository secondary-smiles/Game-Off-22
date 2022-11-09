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
        float parameter = Mathf.InverseLerp(0f, player.maxRecordedSpeed, player.forwardsSpeed);
        parameter = manager.wallRunEasingCurve.Evaluate(parameter);

        float fovAdditive = Mathf.Abs(manager.defaultFov - manager.wallRunFov) * parameter;
        float tiltAdditive = manager.wallRunTilt * parameter;

        manager.currentFov = Mathf.Lerp(manager.currentFov, manager.defaultFov + fovAdditive, manager.timeToWallRunFX * Time.deltaTime);
        camTilt = Mathf.Lerp(camTilt, tiltAdditive * player.wallData.side, manager.timeToWallRunFX * Time.deltaTime);
    }

    private void WallRunFXOff() {
        float parameter = Mathf.InverseLerp(0f, player.maxRecordedSpeed, player.forwardsSpeed);
        parameter = manager.wallRunEasingCurve.Evaluate(parameter);

        manager.currentFov = Mathf.MoveTowards(manager.currentFov, manager.defaultFov, manager.timeToWallRunFX * Time.deltaTime * (1 - parameter) * 10);
        camTilt = Mathf.Lerp(camTilt, 0, manager.timeToWallRunFX * Time.deltaTime);
    }
}
