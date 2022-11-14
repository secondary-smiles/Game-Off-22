using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour {
    WeaponManager manager;
    WeaponAnimationController animator;

    float shotTimer = 0f;

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


        if (CheckReloadConditions()) {
            HandleWeaponReload();
        }
        
        shotTimer -= Time.deltaTime;
        shotTimer = Mathf.Clamp(shotTimer, 0f, manager.activeWeapon.timeBetweenShots);
        
        // print(manager.activeWeapon.ammoInMag);
    }

    private bool CheckReloadConditions() {
        // if (manager.activeWeapon.ammoInMag == 0 && manager.activeWeapon.autoReload) return true;
        // if (manager.isReloading && manager.activeWeapon.ammoInMag < manager.activeWeapon.ammoPerMag) return true;
        if (manager.isReloading) return true;

        return false;
    }

    private void HandleWeaponFire() {
        if (manager.activeWeapon.ammoInMag < 1) return;
        if (shotTimer > 0) return;
        print("fire");
        shotTimer = manager.activeWeapon.timeBetweenShots;

        animator.AnimateWeaponFire();
        manager.activeWeapon.ammoInMag-= 1;
    }


    private void HandleWeaponReload() {
        animator.AnimateWeaponReload();

        int newAmmo = manager.activeWeapon.ammoPerMag;
        if (manager.activeWeapon.totalAmmo < manager.activeWeapon.ammoPerMag) {
            newAmmo = manager.activeWeapon.totalAmmo;
        }
        manager.activeWeapon.ammoInMag = newAmmo;

        if (!manager.activeWeapon.infAmmo) { manager.activeWeapon.totalAmmo -= newAmmo; }
    }
}
