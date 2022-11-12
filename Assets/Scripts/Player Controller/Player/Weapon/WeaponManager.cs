using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {
    [Serializable]
    public struct Weapons {
        public Gun Melee;
        public Gun Pistol;
        public Gun Shotgun;
        public Gun Automatic;
        public Gun Sniper;
    }

    public Weapons weapons;

    [NonSerialized] public Gun activeWeapon;

    Animator playerAnimator;

    // Start is called before the first frame update
    void Start() {
        playerAnimator = GetComponent<Animator>();
        activeWeapon = weapons.Melee;
    }

    private void Update() {
        playerAnimator.runtimeAnimatorController = activeWeapon.animator;

        if (Input.GetKeyDown(KeyCode.E)) { activeWeapon = activeWeapon == weapons.Pistol ? weapons.Melee : weapons.Pistol; };
    }
}
