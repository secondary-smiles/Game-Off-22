using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleSwing : MonoBehaviour {
    public PlayerController player;

    private LineRenderer ropeRenderer;
    private Vector3 grapplePoint;
    
    // Start is called before the first frame update
    void Start() {
        if (ropeRenderer == null) {
            ropeRenderer = gameObject.AddComponent<LineRenderer>();
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(2)) {
            HandleGrapple();
        }
    }

    private void HandleGrapple() {
        Ray ray = new Ray(player.orientation.position, player.orientation.forward);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, player.maxGrappleRange);

        if (CheckRayNotHitTag(hit, "NoGrapple")) {
            
        }
    }

    private bool CheckRayNotHitTag(RaycastHit hit, string tag) {
        return true;
    }
}
