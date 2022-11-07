using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall {
    // -1 left, 1 right, 0 null
    public int side;
    public bool wall { get => side != 0; }
    public Vector3 jumpDirection;

    public Wall(int side) {
        this.side = side;
        this.jumpDirection = Vector3.up;
    }

    public int DetectWall(Transform pos) {
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

    private bool CheckHitIsWall(RaycastHit hit) {
        Collider colliderHit = hit.collider;
        try {
            return colliderHit.GetComponent<Tags>().hasTag("Wall");
        } catch (System.Exception) {
            return false;
        }
    }
}
