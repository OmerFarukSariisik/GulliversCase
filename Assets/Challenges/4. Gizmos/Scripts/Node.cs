using System;
using System.Collections.Generic;
using UnityEngine;

namespace Challenges._4._Gizmos.Scripts
{
    public class Node : MonoBehaviour
    {
        [SerializeField]
        private List<Node> childrenNodes;
        //Edit below

        [SerializeField]
        GizmoData gizmoData;

        private void OnDrawGizmos()
        {
            Gizmos.color = gizmoData.LineColor;

            float arrowHeadLength = 0.7f;
            float arrowHeadAngle = 20.0f;

            foreach (Node node in childrenNodes)
            {
                Vector3 target = node.transform.position;
                Gizmos.DrawLine(transform.position, target);

                Vector3 direction = target - transform.position;
                target = target - direction * 0.1f;

                Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1);
                Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1);

                Gizmos.DrawLine(target, target + right * arrowHeadLength);
                Gizmos.DrawLine(target, target + left * arrowHeadLength);

            }
        }
    }
}
