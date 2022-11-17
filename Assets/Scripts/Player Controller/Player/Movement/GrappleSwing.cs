using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleSwing : MonoBehaviour {
    public PlayerController player;

    private LineRenderer ropeRenderer;
    private SpringJoint joint;

    // Start is called before the first frame update
    void Start() {
        if (ropeRenderer == null) {
            ropeRenderer = gameObject.AddComponent<LineRenderer>();
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(1)) {
            HandleGrapple();
        }
    }

    private void HandleGrapple() {
        (bool, RaycastHit) rayCheck = CheckGrappleRay();
        if (!rayCheck.Item1) return;
        RaycastHit hit = rayCheck.Item2;

        float distance = Vector3.Distance(player.camPos.position, hit.point);

        joint = gameObject.AddComponent<SpringJoint>();
        joint.connectedAnchor = hit.point;
        joint.maxDistance = player.maxPullRangeMultiplier;
        joint.minDistance = player.minPullRangeMultiplier;
        PopulateJoint();

        ropeRenderer.positionCount = 2;
    }

    private (bool, RaycastHit) CheckGrappleRay() {
        Ray ray = new Ray(player.camPos.position, player.camPos.forward);
        RaycastHit hit;
        bool hitSomething = Physics.Raycast(ray, out hit, player.maxGrappleRange);


        if (hitSomething && !CheckRayHitTag(hit, "NoGrapple")) {
            return (true, hit);
        }
        return (false, hit);
    }

    private bool CheckRayHitTag(RaycastHit hit, string tag) {
        try {
            return hit.collider.gameObject.GetComponent<Tags>().hasTag(tag);
        } catch {
            return false;
        }
    }

    private void PopulateJoint() {
        joint.spring = player.ropeSpring;
        joint.damper = player.damper;
        joint.massScale = player.massScale;
    }
    
}
