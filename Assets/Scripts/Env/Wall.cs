using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall {
    // -1 left, 1 right, 0 null
    public float side => DetectWall();
    public bool onWall => side != 0;
    public Vector3 wallNormal;

    Transform pos;
    float maxDistance;
    public Wall(Transform pos, float maxDistance) {
        this.pos = pos;
        this.maxDistance = maxDistance;
    }


    public float DetectWall() {
        float side = 0;
        if (!CheckTagSphere("Wall")) return side;

        float distanceRight = float.PositiveInfinity;
        float distanceLeft = float.PositiveInfinity;

        Ray rayRight = new Ray(pos.position, pos.right);
        Ray rayLeft = new Ray(pos.position, -pos.right);

        RaycastHit hitRight;
        RaycastHit hitLeft;

        Physics.Raycast(rayRight, out hitRight);
        Physics.Raycast(rayLeft, out hitLeft);

        if (CheckHitIsWall(hitRight.collider)) { distanceRight = hitRight.distance; }
        if (CheckHitIsWall(hitLeft.collider)) { distanceLeft = hitLeft.distance; }

        if (distanceLeft < distanceRight) {
            side = -1;
            wallNormal = hitLeft.normal;
        }
        if (distanceRight < distanceLeft) {
            side = 1;
            wallNormal = hitRight.normal;
        }

        return side;
    }

    private bool CheckHitIsWall(Collider hit) {
        try {
            return hit.GetComponent<Tags>().hasTag("Wall");
        } catch (System.Exception) {
            return false;
        }
    }

    private bool CheckTagSphere(string tag) {
        Collider[] hitColliders = Physics.OverlapSphere(pos.position, maxDistance);
        foreach (var hitCollider in hitColliders) {
            if (hitCollider.gameObject.GetComponent<Tags>()) {
                if (hitCollider.gameObject.GetComponent<Tags>().hasTag(tag)) {
                    return true;
                }
            }
        }
        return false;
    }
}
