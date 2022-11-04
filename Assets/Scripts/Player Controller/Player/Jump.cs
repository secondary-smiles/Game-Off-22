using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {
    PlayerController player;

    // Start is called before the first frame update
    void Start() {
        player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Jump")) {
            HandleJump();
        }

        if (player.isGrounded) {
            player.playerDrag = player.groundDrag;
        } else {
            player.playerDrag = player.airDrag;
        }
    }

    private void FixedUpdate() {
        player.playerBody.AddForce(player.playerJumpVelocity, ForceMode.Impulse);
        player.playerJumpVelocity = Vector3.zero;
    }

    void HandleJump() {
        if (!player.isGrounded) return;

        player.playerJumpVelocity = transform.up * player.jump * player.movementMultiplier;
    }
}
