using UnityEngine;

namespace Challenges._3._GGStateMachineCharacterPhysics.Scripts.MonoBehaviours
{
    public class BodyAnimator : MonoBehaviour
    {
        private Vector3 _previousPosition;
        private Vector3 _rotationAxis;
        [SerializeField,Min(0.1f)]
        private float radius;

        private void Start()
        {
            _previousPosition = transform.position;
        }

        void Update()
        {
            var currentPosition = transform.position;
            var delta = currentPosition - _previousPosition;
            var normalizedDelta = delta.normalized;
            var xz = new Vector3(delta.x, 0, delta.z);
            var movementMagnitude = xz.magnitude;
            var radianRotation = movementMagnitude / radius;
            _rotationAxis = Vector3.Cross(Vector3.up, normalizedDelta);
            //var rotation = Quaternion.AngleAxis(radianRotation * Mathf.Rad2Deg, rotationAxis);
            transform.Rotate(_rotationAxis,radianRotation * Mathf.Rad2Deg,Space.World);
            //transform.rotation = transform.rotation * rotation;
            _previousPosition = currentPosition;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position,radius);
            Gizmos.color = Color.black;
            Gizmos.DrawLine(transform.position,transform.position+_rotationAxis*(radius+1f));
        }
    }
}
