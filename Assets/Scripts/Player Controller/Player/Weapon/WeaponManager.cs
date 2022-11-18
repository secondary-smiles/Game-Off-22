using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {
    public UIManager uiManager;
    public Gun[] weapons;

    public int startingWeaponIndex = 0;

    [NonSerialized] public Animator animator;

    [NonSerialized] public Gun activeGun;

    [NonSerialized] public bool isReloading;

    WeaponReload reloadModule;

    int activeGunIndex;

    // Start is called before the first frame update
    void Start() {
        _StartupAddComponents();

        animator = GetComponent<Animator>();
        reloadModule = GetComponent<WeaponReload>();

        SwitchTo(weapons[startingWeaponIndex], animate: false);
    }

    // Update is called once per frame
    void Update() {
        CaptureScrollWheel();
        CaptureNumberRow();
        ClampSelectedWeaponIndex();

        SwitchTo(weapons[activeGunIndex]);
    }

    private void SwitchTo(Gun weapon, bool animate = true) {
        if (activeGun == weapon) return;

        reloadModule.CancelReloadIfActive();
        activeGun = weapon;
        activeGunIndex = Array.IndexOf(weapons, weapon);

        _SwitchToWeaponSetupHelper(weapon);
        StartCoroutine(_SwitchToAnimationHelper(weapon, animate));
    }

    private void _SwitchToWeaponSetupHelper(Gun weapon) {
        if (!activeGun.firstEquip) return;

        activeGun.ammoInMag = activeGun.ammoPerMag;
        activeGun.totalAmmo = activeGun.startingTotalAmmo;

        activeGun.firstEquip = false;
    }

    private IEnumerator _SwitchToAnimationHelper(Gun weapon, bool animate) {
        if (animate) {
            animator.ResetTrigger("WeaponSwitch");
            animator.SetTrigger("WeaponSwitch");

            yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(3).IsName("FPWeapon Switch"));
        }
        animator.runtimeAnimatorController = weapon.animatorController;

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(3).IsName("FPWeapon Switch"));
    }

    private void CaptureNumberRow() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) { activeGunIndex = 0; }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { activeGunIndex = 1; }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { activeGunIndex = 2; }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { activeGunIndex = 3; }
        if (Input.GetKeyDown(KeyCode.Alpha5)) { activeGunIndex = 4; }
        if (Input.GetKeyDown(KeyCode.Alpha6)) { activeGunIndex = 5; }
        if (Input.GetKeyDown(KeyCode.Alpha7)) { activeGunIndex = 6; }
        if (Input.GetKeyDown(KeyCode.Alpha8)) { activeGunIndex = 7; }
        if (Input.GetKeyDown(KeyCode.Alpha9)) { activeGunIndex = 8; }
        if (Input.GetKeyDown(KeyCode.Alpha0)) { activeGunIndex = 9; }
    }

    private void CaptureScrollWheel() {
        Vector2 scroll = Vector2.right * Input.GetAxis("Mouse ScrollWheel");
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
            scroll = Input.mouseScrollDelta;
        }

        if (scroll.x > 0f || scroll.y > 0f) {
            activeGunIndex++;
            if (activeGunIndex > weapons.Length - 1) { activeGunIndex = 0; }
        }
        if (scroll.x < 0f || scroll.y > 0f) {
            activeGunIndex--;
            if (activeGunIndex < 0) { activeGunIndex = weapons.Length - 1; }
        }
    }

    private void ClampSelectedWeaponIndex() {
        activeGunIndex = Mathf.Clamp(activeGunIndex, 0, weapons.Length - 1);
    }

    private void _StartupAddComponents() {
        gameObject.AddComponent<WeaponShoot>().manager = this;
        gameObject.AddComponent<WeaponReload>().manager = this;
    }
}
