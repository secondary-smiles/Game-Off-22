using System.Collections;
using System.Collections.Generic;
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
    void Update() {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        mouseY = yLookInverted ? mouseY : -mouseY;

        xRotation += mouseY * mouseSensitivity;
        xRotation = Mathf.Clamp(yRotation, -90f, 90f);

        yRotation += mouseX * mouseSensitivity;

        playerCam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
