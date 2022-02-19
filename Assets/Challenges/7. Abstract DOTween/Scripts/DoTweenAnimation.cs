using UnityEngine;
using UnityEngine.Serialization;

namespace Challenges._7._Abstract_DOTween.Scripts
{
    public abstract class DoTweenAnimation : AbstractPickUpScript
    {
        [FormerlySerializedAs("pad")]
        [HideInInspector]
        [SerializeField]
        private Transform floorPad;
        [FormerlySerializedAs("obj")]
        [HideInInspector]
        [SerializeField]
        private Transform centerObject;
        [FormerlySerializedAs("objShade")]
        [HideInInspector]
        [SerializeField]
        private Transform centerObjectShade;

        public Transform CenterObjectShade => centerObjectShade;

        public Transform CenterObject => centerObject;

        public Transform FloorPad => floorPad;
        public Material ShadeMaterial { get; private set; }

        private Material cachedSharedMaterial;
        public override void BeforeStart()
        {
            var rendr = CenterObjectShade.GetComponent<Renderer>();
            cachedSharedMaterial = rendr.sharedMaterial;
            ShadeMaterial = new Material(cachedSharedMaterial);
            rendr.sharedMaterial = ShadeMaterial;
        }

        public override void AfterEnd()
        {
            var rendr = CenterObjectShade.GetComponent<Renderer>();
            DestroyImmediate(ShadeMaterial);
            rendr.sharedMaterial = cachedSharedMaterial;
        }
    }
}
