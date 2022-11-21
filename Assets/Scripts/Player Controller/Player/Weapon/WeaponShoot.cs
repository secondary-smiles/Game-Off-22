using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShoot : MonoBehaviour {
    public WeaponManager manager;

    private bool isFiring;
    private bool isAnimating;
    
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
        if (isFiring) return;
        if (manager.isReloading) return;
        if (!manager.activeGun.autoFire && isAnimating) return;
        
        // Shoot
        manager.animator.SetBool("Fire", false);
        
        manager.activeGun.ammoInMag--;
        StartCoroutine(_HandleShootAnimationHelper());
        StartCoroutine(_HandleShootTimerHelper());

        //TODO: Add recoil and FX
    }

    private IEnumerator _HandleShootAnimationHelper() {
        isAnimating = true;
        manager.animator.SetBool("Fire", true);
        yield return new WaitUntil(() => !manager.animator.GetCurrentAnimatorStateInfo(3).IsName("FPWeapon Fire"));
        isAnimating = false;
    }

    private IEnumerator _HandleShootTimerHelper() {
        isFiring = true;
        yield return new WaitForSecondsRealtime(manager.activeGun.timeBetweenShots);
        manager.animator.SetBool("Fire", false);
        isFiring = false;
    }
    
}
