using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public PlayerController player;
    public Image currentCrosshair;
    
    
    // Start is called before the first frame update
    void Start() {
        _StartupAddComponents();
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void _StartupAddComponents() {
        gameObject.AddComponent<Crosshair>().manager = this;
    }
}
