using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour {
    PlayerMovement player;

    // Start is called before the first frame update
    void Start() {
        player = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update() {
        SetGravityVelocity();
    }

    void SetGravityVelocity() {
        player.isGrounded = false;

        Collider[] hitColliders = Physics.OverlapSphere(player.groundCheckPoint.position, player.groundCheckPointRadius);
        foreach (var hitCollider in hitColliders) {
            if (hitCollider.gameObject.GetComponent<Tags>()) {
                if (hitCollider.gameObject.GetComponent<Tags>().hasTag("groundCheck")) {
                    player.isGrounded = true;
                }
            }
        }

        if (player.isGrounded && player.controllerVelocity.y < 0) {
            // Not instantly stopping the velocity makes sure that the player is actually touching the ground
            player.controllerVelocity.y = -2f;
        } else {
            player.controllerVelocity.y += -player.gravityStrength * Time.deltaTime;
        }
    }
}
