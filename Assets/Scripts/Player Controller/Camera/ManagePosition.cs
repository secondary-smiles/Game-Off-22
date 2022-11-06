using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagePosition : MonoBehaviour {
    public Transform targetPosition;
    // Update is called once per frame
    void LateUpdate() {
        transform.position = targetPosition.position;
    }
}
