using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {
    public Gun[] weapons;

    [NonSerialized] public Gun activeWeapon;

    [NonSerialized] public int selectedWeaponIndex = 0;
    [NonSerialized] public bool isFiring = false;
    [NonSerialized] public bool isReloading = false;

    [NonSerialized] public Animator playerAnimator;

    // Start is called before the first frame update
    void Start() {
        _StartupAddComponents();
        playerAnimator = GetComponent<Animator>();

        activeWeapon = weapons[0];
        SwitchTo(activeWeapon);
    }

    private void Update() {
        CaptureInput();
    }

    void LateUpdate() {
        isFiring = false;
        isReloading = false;
    }

    public void SwitchTo(Gun gun) {
        activeWeapon = gun;
        if (activeWeapon.ammoInMag == 0 && activeWeapon.firstEquip == false) {
            activeWeapon.ammoInMag = activeWeapon.ammoPerMag;
        }
        playerAnimator.runtimeAnimatorController = activeWeapon.animator;
    }

    private void CaptureInput() {
        CaptureNumberRow();

        if (Input.GetButtonDown("Fire1")) { isFiring = true; }
        if (Input.GetKeyDown(KeyCode.R)) { isReloading = true; }
    }

    private void CaptureNumberRow() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) { selectedWeaponIndex = 0; }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { selectedWeaponIndex = 1; }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { selectedWeaponIndex = 2; }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { selectedWeaponIndex = 3; }
        if (Input.GetKeyDown(KeyCode.Alpha5)) { selectedWeaponIndex = 4; }
        if (Input.GetKeyDown(KeyCode.Alpha6)) { selectedWeaponIndex = 5; }
        if (Input.GetKeyDown(KeyCode.Alpha7)) { selectedWeaponIndex = 6; }
        if (Input.GetKeyDown(KeyCode.Alpha8)) { selectedWeaponIndex = 7; }
        if (Input.GetKeyDown(KeyCode.Alpha9)) { selectedWeaponIndex = 8; }
        if (Input.GetKeyDown(KeyCode.Alpha0)) { selectedWeaponIndex = 9; }
    }

    private void _StartupAddComponents() {
        gameObject.AddComponent<WeaponAnimationController>();
    }
}
