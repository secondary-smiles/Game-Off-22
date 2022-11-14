using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationController : MonoBehaviour {
    WeaponManager manager;

    // Start is called before the first frame update
    void Start() {
        manager = GetComponent<WeaponManager>();
    }

    void Update() {
        manager.playerAnimator.SetBool("Reload", manager.playerAnimator.GetCurrentAnimatorStateInfo(3).IsName("FPWeapon Reload"));
    }

    public void AnimateWeaponFire() {
        manager.playerAnimator.SetTrigger("Fire");
    }

    public void resetFireAnimation() {
        manager.playerAnimator.ResetTrigger("Fire");
    }

    public void AnimateWeaponReload() {
        manager.playerAnimator.SetBool("Reload", true);
    }
}
