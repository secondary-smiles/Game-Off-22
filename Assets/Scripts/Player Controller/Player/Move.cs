using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    PlayerMovement player;

    // Start is called before the first frame update
    void Start() {
        player = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update() {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        // Scope input to local coordinates
        Vector3 moveVector = CalcForwardsVelocity(xInput, zInput);

        player.controllerVelocity.x = moveVector.x;
        player.controllerVelocity.z = moveVector.z;
    }

    public Vector3 CalcForwardsVelocity(float x, float z) {
        Vector3 moveVector = transform.right * x + transform.forward * z;
        moveVector = player.runSpeed * moveVector;
        return moveVector;
    }
}
