using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAsyncAwaitUtil;

public class FixedJointGizmoDrawer : MonoBehaviour {
    public Color gizmoColor;
    private Transform attachedObject;

    async void Start() {
        if(gizmoColor == null || gizmoColor.a < .01f){ 
            gizmoColor = Color.cyan; 
            }
        FixedJoint2D fixedJoint = FindObjectOfType<FixedJoint2D>();
        while (fixedJoint == null) {
            fixedJoint = FindObjectOfType<FixedJoint2D>();
            await new WaitForUpdate();
        }
        attachedObject = fixedJoint.transform;

    }



    void OnDrawGizmos() {
        if (attachedObject == null) {
            return;
        }
        Vector3 start = transform.position;
        Vector3 end = attachedObject.position;
        Gizmos.color = gizmoColor;
        Gizmos.DrawLine(start, end);

    }

}
