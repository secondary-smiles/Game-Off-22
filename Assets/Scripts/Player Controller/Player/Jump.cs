using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {
    PlayerMovement player;

    // Start is called before the first frame update
    void Start() {
        player = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Jump")) {
            HandleJump();
        }

        HandleCollisionEdges();
    }

    

    void HandleJump() {
        player.targetStepAndSlope(0f, 0f);

        // Guard clause, make sure we're on the ground when jumping
        if (!player.isGrounded) return;

        // Physics says that formula calculates the correct amount of velocity needed
        player.controllerVelocity.y = Mathf.Sqrt(player.jumpHeight * 2 * player.gravityStrength);
    }

    void HandleCollisionEdges() {
        if (player.controller.isGrounded && !player.isGrounded) {
            float direction = 1;
            if (player.controllerVelocity.x < 0) {
                direction = -1;
            }
            Vector3 correctionVector = GetComponent<Move>().CalcForwardsVelocity(0f, direction);
            correctionVector.y = 10;
            Debug.Log(player.controllerVelocity);
            player.controller.Move(correctionVector * Time.deltaTime);
        }
    }
}
