using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slope {
    public Vector3 normal;
    public bool grounded;
    public bool groundedRaw;

    public Slope(bool grounded, bool groundedRaw, Vector3 normal) {
        this.grounded = grounded;
        this.groundedRaw = groundedRaw;
        this.normal = normal;
    }

    public bool onSlope {
        get {
            if (!grounded) return false;
            if (normal != Vector3.up) return true;
            return false;
        }
    }

    public bool onSlopeRaw {
        get {
            if (!groundedRaw) return false;
            if (normal != Vector3.up) return true;
            return false;
        }
    }
}
