using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "ScriptableObjects/Gun")]
public class Gun : ScriptableObject {
    public string gunName;
    public AnimatorOverrideController animator;

    public bool autoReload = true;
}
