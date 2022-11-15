using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReload : MonoBehaviour {
    public WeaponManager manager;
    
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        bool reloadButton = Input.GetKeyDown(KeyCode.R);
        if (CanReload(reloadButton)) {
            print("should reload");
        }
    }

    private bool CanReload(bool reloadButton) {
        if (manager.activeGun.ammoInMag == manager.activeGun.ammoPerMag) return false;
        
        if (manager.activeGun.ammoInMag == 0 && reloadButton) return true;
        if (manager.activeGun.autoReload && manager.activeGun.ammoInMag == 0) return true;

        return false;
    }
}
