using UnityEngine;

namespace Challenges._8._DecouplingAndOptimization.Script
{
    [CreateAssetMenu(fileName = "GameTimeMaterialTintSettings",menuName = "DecouplingChallenge/GameTimeMaterialTintSettings")]
    public class GameTimeMaterialTintSettings : ScriptableObject
    {
        public Color colorAtMidnight;
        public Color colorAt6AM;
        public Color colorAt12PM;
        public Color colorAt6PM;
        public float realSecondsPerGameSecond;
        public int startHour;
        public int startSeconds;
    }
}
