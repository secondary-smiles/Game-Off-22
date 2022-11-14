using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour {
    WeaponManager manager;
    WeaponAnimationController animator;

    float shotTimer = 0f;
    float reloadTimer = 0f;

    // Start is called before the first frame update
    void Start() {
        manager = GetComponent<WeaponManager>();
        animator = GetComponent<WeaponAnimationController>();
    }

    // Update is called once per frame
    void Update() {
        if (!manager.weaponEquipped) return;
        
        if (manager.isFiring) {
            HandleWeaponFire();
        }
        if (!manager.isFiring && manager.activeWeapon.autoFire) {
            animator.resetFireAnimation();
        }


        if (CheckReloadConditions()) {
            HandleWeaponReload();
        }

        shotTimer -= Time.deltaTime;
        shotTimer = Mathf.Clamp(shotTimer, 0f, manager.activeWeapon.timeBetweenShots);
        
        reloadTimer -= Time.deltaTime;
        reloadTimer = Mathf.Clamp(reloadTimer, 0f, manager.activeWeapon.timeToReload);
    }

    private bool CheckReloadConditions() {
        if (manager.activeWeapon.ammoInMag == 0 && manager.activeWeapon.autoReload) return true;
        if (manager.isReloading && manager.activeWeapon.ammoInMag < manager.activeWeapon.ammoPerMag) return true;
        if (reloadTimer > 0f) return false;

        return false;
    }

    private void HandleWeaponFire() {
        if (manager.activeWeapon.ammoInMag < 1) return;
        if (shotTimer > 0 || reloadTimer > 0) return;

        print("fire");
        shotTimer = manager.activeWeapon.timeBetweenShots;

        animator.AnimateWeaponFire();
        manager.activeWeapon.ammoInMag -= 1;
    }


    private void HandleWeaponReload() {
        reloadTimer = manager.activeWeapon.timeToReload;
        
        animator.resetFireAnimation();
        animator.AnimateWeaponReload();

        int newAmmo = manager.activeWeapon.ammoPerMag;
        if (manager.activeWeapon.totalAmmo < manager.activeWeapon.ammoPerMag) {
            newAmmo = manager.activeWeapon.totalAmmo;
        }
        manager.activeWeapon.ammoInMag = newAmmo;

        if (!manager.activeWeapon.infAmmo) { manager.activeWeapon.totalAmmo -= newAmmo; }
    }
}
