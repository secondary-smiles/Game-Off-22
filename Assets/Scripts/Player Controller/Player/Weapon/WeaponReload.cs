using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReload : MonoBehaviour {
    public WeaponManager manager;

    Coroutine reloadCoroutine;

    // Update is called once per frame
    void Update() {
        bool reloadButton = Input.GetKeyDown(KeyCode.R);
        if (CanReload(reloadButton)) {
            HandleReload();
        }
    }

    private bool CanReload(bool reloadButton) {
        if (manager.activeGun.ammoInMag == manager.activeGun.ammoPerMag) return false;

        if (manager.activeGun.ammoInMag < manager.activeGun.ammoPerMag && reloadButton) return true;
        if (manager.activeGun.autoReload && manager.activeGun.ammoInMag == 0) return true;

        return false;
    }

    private void HandleReload() {
        if (manager.isReloading) return;

        manager.animator.SetTrigger("Reload");
        reloadCoroutine = StartCoroutine(_HandleReloadAnimationHelper());
    }

    private void ApplyAmmoCalc() {
        manager.activeGun.ammoInMag = manager.activeGun.ammoPerMag;
        if (manager.activeGun.infAmmo) { manager.activeGun.totalAmmo -= manager.activeGun.ammoPerMag; }
    }

    private IEnumerator _HandleReloadAnimationHelper() {
        manager.isReloading = true;
        yield return new WaitForSeconds(manager.activeGun.reloadTime);
        yield return new WaitUntil(() => !manager.animator.GetCurrentAnimatorStateInfo(3).IsName("FPWeapon Reload"));
        ApplyAmmoCalc();
        manager.isReloading = false;
    }

    public void CancelReloadIfActive() {
        if (!manager.isReloading) return;

        StopCoroutine(reloadCoroutine);
        manager.animator.ResetTrigger("Reload");

        manager.isReloading = false;
    }
}
