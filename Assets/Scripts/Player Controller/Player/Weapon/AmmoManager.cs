using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour {
    WeaponManager manager;
    WeaponAnimationController animator;

    // Start is called before the first frame update
    void Start() {
        manager = GetComponent<WeaponManager>();
        animator = GetComponent<WeaponAnimationController>();
    }

    // Update is called once per frame
    void Update() {
        if (manager.isFiring) {
            HandleWeaponFire();
        }
        if (!manager.isFiring && manager.activeWeapon.autoFire) {
            animator.resetFireAnimation();
        }
    }

    private void HandleWeaponFire() {
        if (manager.activeWeapon.ammoInMag != 0) {
            animator.AnimateWeaponFire();
        }
        

        if ((manager.activeWeapon.ammoInMag == 0 && manager.activeWeapon.autoReload) ||
            (manager.isReloading && manager.activeWeapon.ammoInMag < manager.activeWeapon.ammoPerMag)) {
            animator.AnimateWeaponFire();
        }
    }
}
