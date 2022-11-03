using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public CharacterController controller;
    public Transform groundCheckPoint;

    public float jumpHeight = 3f;
    public float runSpeed = 12f;

    public float gravityStrength = 9.8f;
    public float groundCheckPointRadius = 0.6f;

    [System.NonSerialized]
    public Vector3 controllerVelocity;
    [System.NonSerialized]
    public bool isGrounded;

    private float stepOffsetStore;
    private float slopeLimitStore;

    private void Update() {
        controller.Move(controllerVelocity * Time.deltaTime);
    }

    public void targetStepAndSlope(float step, float slope) {
        stepOffsetStore = controller.stepOffset;
        slopeLimitStore = controller.slopeLimit;

        controller.stepOffset = step;
        controller.slopeLimit = slope;
    }

    public void restoreStepAndSlope() {
        controller.stepOffset = stepOffsetStore;
        controller.slopeLimit = slopeLimitStore;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckPointRadius);
    }
}
