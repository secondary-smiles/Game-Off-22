    using UnityEngine;

public class GrappleSwing : MonoBehaviour {
    public PlayerController player;

    private LineRenderer ropeRenderer;
    private SpringJoint joint;
    private Vector3 grapplePos;
    private Vector3 grappleHitPoint;

    // Start is called before the first frame update
    void Start() {
        ropeRenderer = gameObject.GetComponent<LineRenderer>();
        ropeRenderer.positionCount = 0;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(1)) {
            HandleGrappleStart();
        }

        if (Input.GetMouseButtonUp(1)) {
            HandleGrappleStop();
        }
    }

    private void LateUpdate() {
        DrawRope();
    }

    private void HandleGrappleStart() {
        (bool, RaycastHit) rayCheck = CheckGrappleRay();
        if (!rayCheck.Item1) return;
        
        player.playerAnimator.SetBool("Grappling", true);
        
        RaycastHit hit = rayCheck.Item2;

        grappleHitPoint = hit.point;

        float distance = Vector3.Distance(player.camPos.position, grappleHitPoint);

        joint = gameObject.AddComponent<SpringJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = grappleHitPoint;
        joint.maxDistance = player.maxPullRangeMultiplier * distance;
        joint.minDistance = player.minPullRangeMultiplier * distance;
        PopulateJoint();

        ropeRenderer.positionCount = 2;

        grapplePos = player.grappleShootPoint.position;
    }

    private void HandleGrappleStop() {
        player.playerAnimator.SetBool("Grappling", false);
        ropeRenderer.positionCount = 0;
        Destroy(joint);
    }

    private (bool, RaycastHit) CheckGrappleRay() {
        Ray ray = new Ray(player.camPos.position, player.camPos.forward);
        RaycastHit hit;
        bool hitSomething = Physics.SphereCast(ray, player.grappleAimAssist, out hit, player.maxGrappleRange);

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

    private void DrawRope() {
        if (!joint) return;

        grapplePos = Vector3.Lerp(grapplePos, grappleHitPoint, Time.deltaTime * player.grappleLerpSpeedMultiplier);

        ropeRenderer.SetPosition(0, player.grappleShootPoint.position);
        ropeRenderer.SetPosition(1, grapplePos);
    }
}
