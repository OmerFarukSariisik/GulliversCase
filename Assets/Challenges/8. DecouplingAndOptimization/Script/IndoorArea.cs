using System;
using UnityEngine;

namespace Challenges._8._DecouplingAndOptimization.Script
{
    [RequireComponent(typeof(BoxCollider))]
    public class IndoorArea : MonoBehaviour
    {
        public const int IndoorAreaLayer = 11;
        private BoxCollider _collider;

        private void Start()
        {
            _collider = GetComponent<BoxCollider>();
            if (_collider == null)
            {
                throw new Exception("IndoorArea is missing the BoxCollider component.");
            }

            _collider.isTrigger = true;
            gameObject.layer = IndoorAreaLayer;
        }

        private void OnDrawGizmos()
        {
            if (_collider == null) _collider = GetComponent<BoxCollider>();
            if (_collider == null) return;
            Gizmos.matrix = transform.localToWorldMatrix;
            var col = Color.blue;
            col.a = 0.2f;
            Gizmos.color = col;
            Gizmos.DrawCube(_collider.center,_collider.size);
        }
    }
}
