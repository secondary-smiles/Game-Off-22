using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "ScriptableObjects/Gun")]
public class Gun : ScriptableObject {
    public string gunName;
    public int index;
    public AnimatorOverrideController animator;

    public bool autoReload = true;

    [Header("Gun Settings")]
    public float ammoPerMag;
    public float startingTotalAmmo;

    // Sys data
    [NonSerialized] public bool firstEquip;
    [NonSerialized] public float ammoInMag;
    [NonSerialized] public float totalAmmo;
}
