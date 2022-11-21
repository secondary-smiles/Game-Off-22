using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy")]
public class Enemy : ScriptableObject {
    public string enemyName;

    public float startingHealth;

    public float viewRange;
    public float fovAngle;
    
    public enum State {
        Idle,
        Chase,
        Attack,
        Run
    }

    [NonSerialized] public float currentHealth;
}
