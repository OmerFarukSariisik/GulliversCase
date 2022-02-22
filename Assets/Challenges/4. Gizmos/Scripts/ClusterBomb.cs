using System;
using UnityEditor;
using UnityEngine;

namespace Challenges._4._Gizmos.Scripts
{
    public class ClusterBomb : MonoBehaviour
    {
        [SerializeField]
        private ClusterBombData clusterBombData;
        //Edit below

        [SerializeField]
        GizmoData gizmoData;

        Vector3 lastPos;
        Vector3 nextPos = new Vector3();
        float distance;

        private void OnDrawGizmos()
        {
            lastPos = transform.position;

            float travelEachBounce = clusterBombData.ChildTravelDistance / clusterBombData.ChildBounceCount;
            float peek = travelEachBounce / 2f + 1;

            for (int i = 0; i < clusterBombData.ChildCount; i++)
            {
                for (int j = 1; j <= clusterBombData.ChildBounceCount; j++)
                {
                    float radians = 2 * Mathf.PI / clusterBombData.ChildCount * i;

                    float vertical = Mathf.Sin(radians);
                    float horizontal = Mathf.Cos(radians);

                    nextPos.x = horizontal;
                    nextPos.z = vertical;
                    
                    nextPos *= travelEachBounce;


                    Vector3 startPoint = lastPos;
                    Vector3 endPoint = lastPos + nextPos;
                    Vector3 startTangent = lastPos;

                    
                    float height = clusterBombData.ChildInitialJumpHeight * Mathf.Pow(distance + peek, -clusterBombData.ChildBounceFalloff);
                    Vector3 endTangent = lastPos + nextPos / 2 + height * Vector3.up;

                    Handles.DrawBezier(startPoint, endPoint, startTangent, endTangent, gizmoData.BezierColor, null, gizmoData.BezierWidth);

                    if (j == clusterBombData.ChildBounceCount)
                        DrawSphere(endPoint, clusterBombData.ChildExplosionRadius, clusterBombData.ChildDamage);

                    lastPos = endPoint;
                    distance += travelEachBounce;
                }
                
                lastPos = transform.position;
                distance = 0f;
            }



            DrawSphere(transform.position, clusterBombData.SelfExplosionRadius, clusterBombData.SelfDamage);
            
        }

        private void DrawSphere(Vector3 pos, float radius, float damage)
        {
            Gizmos.color = gizmoData.SphereColor;
            Gizmos.DrawWireSphere(pos, radius);

            GUIStyle gUIStyle = new GUIStyle();
            gUIStyle.normal.textColor = gizmoData.TextColor;
            gUIStyle.fontSize = gizmoData.FontSize;
            gUIStyle.fontStyle = FontStyle.Bold;
            gUIStyle.alignment = TextAnchor.MiddleCenter;

            Handles.Label(pos, damage.ToString(), gUIStyle);
        }
    }
}
