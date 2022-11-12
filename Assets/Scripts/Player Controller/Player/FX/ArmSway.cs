using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmSway : MonoBehaviour {
    public PlayerController player;

    [Header("Sway")]
    public float smoothing = 8f;
    public float swayMultiplier = 2f;

    float velX;
    float velY;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        CaptureInput();

        Quaternion rotationX = Quaternion.AngleAxis(-velY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(velX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smoothing * Time.deltaTime);
    }

    private void CaptureInput() {
        velX = player.playerBody.velocity.x * swayMultiplier;
        velY = player.playerBody.velocity.y * swayMultiplier;
    }
}
