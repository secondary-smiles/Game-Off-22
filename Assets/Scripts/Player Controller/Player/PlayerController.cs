using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [Header("Movement")]
    public float movementSpeed = 6f;
    public float movementDrag = 6f;
    public float movementMultiplier = 10f;

    [System.NonSerialized] public Vector3 moveDirection;
    [System.NonSerialized] public Rigidbody playerBody;

    [System.NonSerialized] public float horizontalMovement;
    [System.NonSerialized] public float verticalMovement;

    private float _currentDrag;
    public float currentDrag {
        get => _currentDrag;
        set {
            _currentDrag = value;
            playerBody.drag = value;
        }
    }

    private void Start() {
        _StartupAddComponents();
        playerBody = GetComponent<Rigidbody>();
        playerBody.freezeRotation = true;
    }


    private void Update() {
        CaptureInput();
        currentDrag = movementDrag;
    }

    private void CaptureInput() {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
    }

    private void _StartupAddComponents() {
        gameObject.AddComponent<Walk>();
    }
}
