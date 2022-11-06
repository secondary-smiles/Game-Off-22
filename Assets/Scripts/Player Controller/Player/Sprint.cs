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
            player.moveSpeed = Mathf.Lerp(player.walkSpeed, player.sprintSpeed, player.timeToSprint * Time.deltaTime * player.movementMultiplier);
        } else {
            player.moveSpeed = Mathf.Lerp(player.sprintSpeed, player.walkSpeed, player.timeToSprint * Time.deltaTime * player.movementMultiplier);
        }
    }
}
