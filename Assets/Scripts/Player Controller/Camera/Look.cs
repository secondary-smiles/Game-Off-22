using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour {

    public float sensitivity = 100f;
    public bool yInverted = false;

    [SerializeField] Transform cam;
    [SerializeField] Transform orientation;

    float dampener = 0.01f;

    float mouseX;
    float mouseY;

    float xRotation;
    float yRotation;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {
        CaptureInput();

        cam.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    private void CaptureInput() {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * sensitivity * dampener;

        float direction = yInverted ? -1f : 1f;
        xRotation -= mouseY * sensitivity * dampener * direction;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
    }
}
