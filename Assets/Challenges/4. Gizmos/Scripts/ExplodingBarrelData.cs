using UnityEngine;

namespace Challenges._4._Gizmos.Scripts
{
    public enum DamageType
    {
        Water,
        Earth,
        Fire,
        Air,
        LongAgoTheFourNationsLivedTogetherInHarmony
    }
    [CreateAssetMenu(fileName = "ExplodingBarrelData",menuName = "GizmoChallenge/ExplodingBarrelData")]
    public class ExplodingBarrelData : ScriptableObject
    {
        [SerializeField]
        private int damage;
        [SerializeField]
        private DamageType damageType;
        [SerializeField]
        private float explosionRadius;

        public float ExplosionRadius => explosionRadius;

        public DamageType DamageType => damageType;

        public int Damage => damage;
    }
}
