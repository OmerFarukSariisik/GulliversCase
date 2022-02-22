using System;
using UnityEditor;
using UnityEngine;

namespace Challenges._4._Gizmos.Scripts
{
    public class ExplodingBarrel : MonoBehaviour
    {
        [SerializeField]
        private ExplodingBarrelData explodingBarrelData;
        //Edit below

        [SerializeField]
        GizmoData gizmoData;

        private void OnDrawGizmos()
        {
            switch (explodingBarrelData.DamageType)
            {
                case DamageType.Water:
                    Gizmos.color = Color.cyan;
                    break;
                case DamageType.Earth:
                    Gizmos.color = Color.green;
                    break;
                case DamageType.Fire:
                    Gizmos.color = Color.red;
                    break;
                case DamageType.Air:
                    Gizmos.color = Color.blue;
                    break;
                case DamageType.LongAgoTheFourNationsLivedTogetherInHarmony:
                    Gizmos.color = Color.white;
                    break;
                default:
                    break;
            }
            
            Gizmos.DrawWireSphere(transform.position, explodingBarrelData.ExplosionRadius);

            GUIStyle gUIStyle = new GUIStyle();
            gUIStyle.normal.textColor = gizmoData.TextColor;
            gUIStyle.fontSize = gizmoData.FontSize;
            gUIStyle.fontStyle = FontStyle.Bold;
            gUIStyle.alignment = TextAnchor.MiddleCenter;

            Handles.Label(transform.position, explodingBarrelData.Damage.ToString(), gUIStyle);
        }
    }
}
