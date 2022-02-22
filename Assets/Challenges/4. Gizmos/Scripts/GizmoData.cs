using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GizmoData", menuName = "GizmoChallenge/GizmoData")]
public class GizmoData : ScriptableObject
{
    [SerializeField]
    private Color textColor;
    [SerializeField]
    private Color sphereColor;
    [SerializeField]
    private Color bezierColor;
    [SerializeField]
    private Color lineColor;

    [SerializeField]
    private int fontSize;
    [SerializeField]
    private float bezierWidth;

    public Color TextColor => textColor;
    public Color SphereColor => sphereColor;
    public Color BezierColor => bezierColor;
    public Color LineColor => lineColor;
    public int FontSize => fontSize;
    public float BezierWidth => bezierWidth;

}
