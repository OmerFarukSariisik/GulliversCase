using System;
using DG.Tweening;
using UnityEngine;

namespace Challenges._7._Abstract_DOTween.Scripts
{
    
    public class PickUpAnimator : DoTweenAnimation
    {
        

        public override Tween StartPreview()
        {
            var sequence = DOTween.Sequence();
            return sequence;
        }
    }
}
