using System;
using UnityEditor;
using UnityEngine;

namespace Challenges._4._Gizmos.Scripts
{
    public class BezierCurve : MonoBehaviour
    {
        [SerializeField]
        private Transform point1;
        [SerializeField]
        private Transform handle1;
        [SerializeField]
        private Transform point2;
        [SerializeField]
        private Transform handle2;
        //Edit below

        [SerializeField]
        GizmoData gizmoData;

        private void OnDrawGizmos()
        {
            Vector3 startPoint = point1.position;
            Vector3 endPoint = point2.position;
            Vector3 startTangent = handle1.position;
            Vector3 endTangent = handle2.position;

            Handles.DrawBezier(startPoint, endPoint, startTangent, endTangent, gizmoData.BezierColor, null, gizmoData.BezierWidth);
        }
    }
}
