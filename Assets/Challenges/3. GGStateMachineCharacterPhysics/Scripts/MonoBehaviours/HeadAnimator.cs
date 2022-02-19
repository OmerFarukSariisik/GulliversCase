using UnityEngine;

namespace Challenges._3._GGStateMachineCharacterPhysics.Scripts.MonoBehaviours
{
    public class HeadAnimator : MonoBehaviour, IInputListener
    {
        private Vector3 _defaultLocalPosition;
        private Vector3 _targetPosition;

        [SerializeField]
        private Transform headTransform;
        [SerializeField]
        private float lerpSpeed;
        [SerializeField]
        private float headDistance;
        // Start is called before the first frame update
        void Start()
        {
            _defaultLocalPosition = headTransform.localPosition;
        }

       

        public void SetCurrentMovement(Vector2 xzPlaneMovementVector)
        {
            _targetPosition = _defaultLocalPosition+xzPlaneMovementVector.ToXZPlane()*headDistance;
        }

        private Vector3 _rotationVector;
        
        private void Update()
        {
            headTransform.localPosition =
                Vector3.Lerp(headTransform.localPosition, _targetPosition, lerpSpeed * Time.deltaTime);
        }
    }
}
