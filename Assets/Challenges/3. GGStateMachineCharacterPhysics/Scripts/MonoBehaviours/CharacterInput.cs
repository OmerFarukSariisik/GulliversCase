using UnityEngine;

namespace Challenges._3._GGStateMachineCharacterPhysics.Scripts.MonoBehaviours
{
    public interface IInputListener
    {
        void SetCurrentMovement(Vector2 xzPlaneMovementVector);
    }
    [RequireComponent(typeof(IInputListener))]
    public class CharacterInput : MonoBehaviour
    {
        private Vector2 _movementVector;
        private IInputListener[] _inputListeners;

        private void Awake()
        {
            _inputListeners = GetComponentsInChildren<IInputListener>();
        }

        void Update()
        {
            _movementVector = Vector2.zero;
            if (Input.GetKey(KeyCode.A)) _movementVector += Vector2.left;
            if (Input.GetKey(KeyCode.S)) _movementVector += Vector2.down;
            if (Input.GetKey(KeyCode.D)) _movementVector += Vector2.right;
            if (Input.GetKey(KeyCode.W)) _movementVector += Vector2.up;

            foreach (var listener in _inputListeners)
            {
                listener.SetCurrentMovement(_movementVector);
            }
        }
    }
}
