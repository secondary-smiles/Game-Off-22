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

        selectedWeaponIndex = Mathf.Clamp(selectedWeaponIndex, 0, weapons.Length - 1);
        SwitchTo(weapons[selectedWeaponIndex]);
    }

    void LateUpdate() {
        isFiring = false;
        isReloading = false;
    }

    public void SwitchTo(Gun gun) {
        if (gun == activeWeapon) return;
        activeWeapon = gun;
        if (activeWeapon.ammoInMag == 0 && !activeWeapon.firstEquip) {
            activeWeapon.ammoInMag = activeWeapon.ammoPerMag;
            activeWeapon.totalAmmo = activeWeapon.startingTotalAmmo;
        }
        playerAnimator.SetTrigger("WeaponSwitch");
        playerAnimator.runtimeAnimatorController = activeWeapon.animator;
        playerAnimator.SetBool("WeaponEquipped", true);
    }

    private void CaptureInput() {
        CaptureScrollWheel();

        // However unlikely, number row has priority over scroll wheel
        CaptureNumberRow();

        if (activeWeapon == null) return;
        isFiring = activeWeapon.autoFire ? Input.GetButton("Fire1") : Input.GetButtonDown("Fire1");
        isReloading = Input.GetKeyDown(KeyCode.R);
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

    private void CaptureScrollWheel() {
        Vector2 scroll = Vector2.right * Input.GetAxis("Mouse ScrollWheel");
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
            scroll = Input.mouseScrollDelta;
        }

        if (scroll.x > 0f || scroll.y > 0f) {
            selectedWeaponIndex++;
            if (selectedWeaponIndex > weapons.Length - 1) { selectedWeaponIndex = 0; }
        }
        if (scroll.x < 0f || scroll.y > 0f) {
            selectedWeaponIndex--;
            if (selectedWeaponIndex < 0) { selectedWeaponIndex = weapons.Length - 1; }
        }
    }

    private void _StartupAddComponents() {
        gameObject.AddComponent<WeaponAnimationController>();
        gameObject.AddComponent<AmmoManager>();
    }
}
