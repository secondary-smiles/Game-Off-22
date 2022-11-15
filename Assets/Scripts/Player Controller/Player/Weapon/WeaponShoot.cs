using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShoot : MonoBehaviour {
    public WeaponManager manager;

    private bool isFiring;
    
    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() {
        bool fire = manager.activeGun.autoFire ? Input.GetButton("Fire1") : Input.GetButtonDown("Fire1");
        if (fire) {
            HandleShoot();
        }
    }

    private void HandleShoot() {
        // Check ammo first
        if (manager.activeGun.ammoInMag <= 0) return;
        if (isFiring || manager.isReloading) return;
        
        // Shoot
        manager.animator.ResetTrigger("Fire");
        manager.animator.SetTrigger("Fire");
        manager.activeGun.ammoInMag--;
        StartCoroutine(_HandleShootAnimationHelper());

        //TODO: Add recoil and FX
    }

    private IEnumerator _HandleShootAnimationHelper() {
        isFiring = true;
        yield return new WaitForSecondsRealtime(manager.activeGun.timeBetweenShots);
        // Just in case fire animation is too long
        yield return new WaitUntil(() => !manager.animator.GetCurrentAnimatorStateInfo(3).IsName("FPWeapon Fire"));
        isFiring = false;
    }
    
}
