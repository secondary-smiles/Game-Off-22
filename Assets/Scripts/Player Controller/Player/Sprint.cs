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
            player.moveSpeed = player.sprintSpeed;
        } else {
            player.moveSpeed = player.walkSpeed;
        }
    }
}
