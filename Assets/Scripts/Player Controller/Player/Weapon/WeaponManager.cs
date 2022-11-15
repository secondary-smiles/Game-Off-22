using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class WeaponManager : MonoBehaviour {
    public Gun[] weapons;

    [NonSerialized] public Animator animator;
    
    [NonSerialized] public Gun activeGun;
    [NonSerialized] public int activeGunIndex;


    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
        SwitchTo(weapons[1]);
    }

    // Update is called once per frame
    void Update() {
        CaptureNumberRow();
        ClampSelectedWeaponIndex();
        
        SwitchTo(weapons[activeGunIndex]);
    }


    private void SwitchTo(Gun weapon) {
        if (activeGun == weapon) return;
        
        activeGun = weapon;
        activeGunIndex = System.Array.IndexOf(weapons, weapon);

        StartCoroutine(_SwitchToAnimationHelper(weapon));
    }    
    
    IEnumerator _SwitchToAnimationHelper(Gun weapon) {
        animator.SetTrigger("WeaponSwitch");

        yield return new WaitUntil(() => !animator.GetCurrentAnimatorStateInfo(3).IsName("FPWeapon Reload"));
        animator.runtimeAnimatorController = weapon.animatorController;
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

    private void ClampSelectedWeaponIndex() {
        activeGunIndex = Mathf.Clamp(activeGunIndex, 0, weapons.Length - 1);
    }
}
