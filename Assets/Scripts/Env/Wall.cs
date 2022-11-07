using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall {
    // -1 left, 1 right, 0 null
    public int side;
    public bool wall { get => side != 0; }
    public Vector3 jumpDirection {
        get => pos.up + DetectWallNormal();
    }

    private Transform pos;

    public Wall(int side, Transform pos) {
        this.side = side;
        this.pos = pos;
    }

    public int DetectWall() {
        float distanceRight = 0f;
        float distanceLeft = 0f;
        Ray rayRight = new Ray(pos.position, pos.right);
        Ray rayLeft = new Ray(pos.position, -pos.right);
        RaycastHit hitRight;
        RaycastHit hitLeft;

        Physics.Raycast(rayRight, out hitRight);
        Physics.Raycast(rayLeft, out hitLeft);

        if (CheckHitIsWall(hitRight)) { distanceRight = hitRight.distance; }
        if (CheckHitIsWall(hitLeft)) { distanceLeft = hitLeft.distance; }

        if (distanceLeft < distanceRight) return -1;
        if (distanceRight < distanceLeft) return 1;
        return 0;
    }

    private Vector3 DetectWallNormal() {
        if (side == 0) return Vector3.up;

        Vector3 direction = side == 1 ? pos.right : -pos.right;
        Ray ray = new Ray(pos.position, direction);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        return hit.normal;
    }

    private bool CheckHitIsWall(RaycastHit hit) {
        Collider colliderHit = hit.collider;
        try {
            return colliderHit.GetComponent<Tags>().hasTag("Wall");
        } catch (System.Exception) {
            return false;
        }
    }
}
