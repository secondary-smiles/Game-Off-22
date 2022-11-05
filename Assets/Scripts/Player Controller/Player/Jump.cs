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
        if (player.isGrounded) {
            player.playerDrag = player.groundDrag;
        } else {
            player.playerDrag = player.airDrag;
        }

        if (Input.GetButtonDown("Jump")) {
            HandleJump();
        }

    }

    private void FixedUpdate() {
        if (player.playerJumpVelocity != Vector3.zero) {
            player.playerBody.AddForce(player.playerJumpVelocity, ForceMode.VelocityChange);
            player.playerJumpVelocity = Vector3.zero;
        }
    }

    void HandleJump() {
        if (!player.isGrounded) return;
        //player.playerJumpVelocity = player.jump * player.movementMultiplier * transform.up;
        player.playerJumpVelocity.y = Mathf.Sqrt((player.jump * player.movementMultiplier) * 2 * player.gravity);
    }
}
