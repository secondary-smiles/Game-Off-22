using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CameraMouseLook : MonoBehaviour {
    public Transform playerCam;
    public Transform orientation;

    public float mouseSensitivity = 1000f;
    public bool yLookInverted = false;

    float xRotation = 0f;
    float yRotation = 0f;

    // Start is called before the first frame update
    void Start() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate() {
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");
        mouseY = yLookInverted ? -mouseY : mouseY;

        yRotation += mouseX * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY * mouseSensitivity * Time.deltaTime;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
