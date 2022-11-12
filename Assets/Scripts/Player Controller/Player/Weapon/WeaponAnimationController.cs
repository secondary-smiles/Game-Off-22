using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationController : MonoBehaviour {
    WeaponManager manager;

    // Start is called before the first frame update
    void Start() {
        manager = GetComponent<WeaponManager>();
        manager.playerAnimator.SetBool("WeaponEquipped", true);
    }

    // Update is called once per frame
    void Update() {
        HandleInput();
    }

    private void HandleInput() {
        manager.selectedWeaponIndex = Mathf.Clamp(manager.selectedWeaponIndex, 0, manager.weapons.Length - 1);
        if (manager.activeWeapon != manager.weapons[manager.selectedWeaponIndex]) {
            manager.SwitchTo(manager.weapons[manager.selectedWeaponIndex]);
        }

        if (manager.isFiring) {
            if (manager.activeWeapon.ammoInMag != 0) {
                manager.playerAnimator.SetTrigger("Fire");
            }
        } else if (manager.isReloading) {
            if (manager.activeWeapon.ammoInMag < 1) {
                manager.playerAnimator.SetTrigger("Reload");
            }
        }
    }
}
