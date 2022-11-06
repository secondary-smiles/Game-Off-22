using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slope {
    public Vector3 normal;
    public bool grounded;

    public Slope(bool grounded, Vector3 normal) {
        this.grounded = grounded;
        this.normal = normal;
    }

    public bool onSlope {
        get {
            if (!grounded) return false;
            if (normal != Vector3.up) return true;
            return false;
        }
    }
}
