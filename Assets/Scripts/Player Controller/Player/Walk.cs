using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour {
    PlayerController player;
    // Start is called before the first frame update
    void Start() {
        player = GetComponent<PlayerController>();
    }

    private void Update() {
        player.moveDirection = transform.forward * player.verticalMovement + transform.right * player.horizontalMovement;
    }

    private void FixedUpdate() {
        Vector3 velocity = player.moveDirection.normalized * player.movementSpeed * player.movementMultiplier;
        player.playerBody.AddForce(velocity, ForceMode.Acceleration);
    }
}
