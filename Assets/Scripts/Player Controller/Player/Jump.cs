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
    }

    private void HandleJump() {
        if (!player.slopeData.grounded && !player.wallData.onWall) return;

        player.currentDrag = player.airDrag;
        player.movementMultiplier = player.airMovementMultiplier;
        Vector3 velocity = player.jumpDirection * player.jumpStrength;
        player.playerBody.AddForce(velocity, ForceMode.Impulse);
    }
}
