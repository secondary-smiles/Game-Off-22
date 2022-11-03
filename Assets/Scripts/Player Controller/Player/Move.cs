using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    PlayerController player;
    // Start is called before the first frame update
    void Start() {
        player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update() {
        player.playerBody.drag = player.drag;

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.forward * moveZ + transform.right * moveX;
        player.playerVelocity = moveDirection;
    }
}
