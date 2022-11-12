using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "ScriptableObjects/Gun")]
public class GunObject : ScriptableObject {
    public string gunName;
    public AnimatorOverrideController animator;
    public AnimationClip idle;
    public AnimationClip fire;
    public AnimationClip reload;
}
