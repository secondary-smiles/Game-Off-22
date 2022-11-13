using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "ScriptableObjects/Gun")]
public class Gun : ScriptableObject {
    public string gunName;
    public AnimatorOverrideController animator;

    public bool autoReload = true;
    public bool autoFire = false;

    public bool infAmmo = false;

    [Header("Gun Settings")]
    public int ammoPerMag;
    public int startingTotalAmmo;

    // Sys data
    [NonSerialized] public bool firstEquip;
    [NonSerialized] public float ammoInMag;
    [NonSerialized] public float totalAmmo;
}
