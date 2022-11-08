using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprint : MonoBehaviour {
    PlayerController player;
    // Start is called before the first frame update
    void Start() {
        player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.LeftShift)) {
            HandleSprintOn();
        } else {
            HandleSprintOff();
        }
    }

    private void HandleSprintOn() {
        player.currentMovementSpeed = Mathf.Lerp(player.currentMovementSpeed, player.sprintSpeed, player.timeToSprint * Time.deltaTime);
    }

    private void HandleSprintOff() {
        player.currentMovementSpeed = Mathf.Lerp(player.currentMovementSpeed, player.walkSpeed, player.timeToSprint * Time.deltaTime);
    }
}
