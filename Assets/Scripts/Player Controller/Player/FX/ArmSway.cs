using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmSway : MonoBehaviour {
    [Header("Sway")]
    public float smoothing = 8f;
    public float swayMultiplier = 2f;

    float mouseX;
    float mouseY;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        CaptureInput();

        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smoothing * Time.deltaTime);
    }

    private void CaptureInput() {
        mouseX = Input.GetAxisRaw("Mouse X") * swayMultiplier;
        mouseY = Input.GetAxisRaw("Mouse Y") * swayMultiplier;
    }
}
