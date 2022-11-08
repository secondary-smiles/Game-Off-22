using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour {
    public PlayerController player;

    [Header("Defaults")]
    public float camFov = 60f;

    [Header("Wall Run FX")]

    [System.NonSerialized] public Camera cam;

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
        currentFov = camFov;

        _StartupAddComponents();
    }

    private void _StartupAddComponents() {
        gameObject.AddComponent<WallRunFX>().player = player;
    }
}
