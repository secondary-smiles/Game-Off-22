using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour {
    public PlayerController player;

    [Header("Defaults")]
    public float defaultFov = 80f;

    [Header("Wall Run FX")]
    public float wallRunActivateSpeed = 52f;
    public float wallRunFov = 90f;
    public float timeToWallRunFov = 200f;

    [System.NonSerialized] public Camera cam;

    [SerializeField] private float _currentFov;
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
