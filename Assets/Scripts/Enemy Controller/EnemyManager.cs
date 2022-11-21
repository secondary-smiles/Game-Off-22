using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    public Transform player;
    public Enemy enemy;


    private bool canSeePlayer => CheckCanSeePlayer();
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        print(canSeePlayer);
    }

    private bool CheckCanSeePlayer() {
        Ray ray = new Ray(transform.position, (player.position - transform.position));
        RaycastHit hit;
        bool hitSomething = Physics.Raycast(ray, out hit, enemy.viewRange);

        if (hitSomething) {
            if (CheckHitTag(hit)) {
                return CheckHitAngle();
            }
        }
        return false;
    }

    private bool CheckHitTag(RaycastHit hit) {
        try {
            return hit.collider.gameObject.GetComponent<Tags>().hasTag("Player");
        } catch {
            return false;
        }
    }

    private bool CheckHitAngle() {
        Vector3 direction = player.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);

        if (angle <= enemy.fovAngle) {
            return true;
        }

        return false;
    }
}
