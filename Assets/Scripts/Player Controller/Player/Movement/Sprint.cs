using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprint : MonoBehaviour {
    public PlayerController player;

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.LeftShift)) {
            HandleSprintOn();
        } else {
            HandleSprintOff();
        }
    }

    private void HandleSprintOn() {
        if (!player.slopeData.grounded) return;
        player.currentMovementSpeed = Mathf.Lerp(player.currentMovementSpeed, player.sprintSpeed, player.timeToSprint * Time.deltaTime);
    }

    private void HandleSprintOff() {
        player.currentMovementSpeed = Mathf.Lerp(player.currentMovementSpeed, player.walkSpeed, player.timeToSprint * Time.deltaTime);
    }
}
