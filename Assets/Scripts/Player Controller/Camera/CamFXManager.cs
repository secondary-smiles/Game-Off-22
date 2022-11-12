using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFXManager : MonoBehaviour {
    public PlayerController player;

    [Header("Defaults")]
    public float defaultFov = 80f;

    [Header("Wall Run FX")]
    public float wallRunFov = 90f;
    public float wallRunTilt = 5f;
    public float timeToWallRunFX = 5f;
    public AnimationCurve wallRunEasingCurve;

    [NonSerialized] public Camera cam;

    private float _currentFov;
    public float currentFov {
        get => _currentFov;
        set {
            _currentFov = value;
            cam.fieldOfView = value;
        }
    }

    // Start is called before the first frame update
    void Start() {
        cam = GetComponentInChildren<Camera>();
        currentFov = defaultFov;

        _StartupAddComponents();
    }

    private void _StartupAddComponents() {
        gameObject.AddComponent<WallRunFX>().player = player;
    }
}
