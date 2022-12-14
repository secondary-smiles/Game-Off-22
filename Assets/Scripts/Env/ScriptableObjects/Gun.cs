using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "ScriptableObjects/Gun")]
public class Gun : ScriptableObject {
    public string gunName;
    public AnimatorOverrideController animatorController;

    public bool autoReload = true;
    public bool autoFire;

    public bool infAmmo;

    [Header("Gun Settings")]
    public int ammoPerMag;
    public int startingTotalAmmo;
    public float timeBetweenShots;
    public float reloadTime;
    
    // Sys data
    [NonSerialized] public bool firstEquip = true;
    [NonSerialized] public int ammoInMag;
    [NonSerialized] public int totalAmmo;
}
