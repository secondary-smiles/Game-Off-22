using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {
    PlayerController player;

    int jumpsLeft;
    // Start is called before the first frame update
    void Start() {
        player = GetComponent<PlayerController>();
        jumpsLeft = player.extraJumps;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Jump")) {
            HandleJump();
        }
        if (player.slopeData.grounded || player.wallData.onWall) {
            jumpsLeft = player.extraJumps;
        }
    }

    private void HandleJump() {
        if (!player.slopeData.grounded && !player.wallData.onWall && jumpsLeft <= 0) return;

        player.playerBody.velocity = new Vector3(player.playerBody.velocity.x, 0, player.playerBody.velocity.z);
        player.currentDrag = player.airDrag;
        player.movementMultiplier = player.airMovementMultiplier;
        Vector3 velocity = player.jumpDirection * player.jumpStrength;
        player.playerBody.AddForce(velocity, ForceMode.Impulse);
        jumpsLeft--;
    }
}
