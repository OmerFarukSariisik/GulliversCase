using Challenges._3._GGStateMachineCharacterPhysics.Scripts.States;
using GGPlugins.GGStateMachine.Scripts.Abstract;
using UnityEngine;

namespace Challenges._3._GGStateMachineCharacterPhysics.Scripts.MonoBehaviours
{
    public class FlowerDetector : MonoBehaviour, IStateMachineUser
    {
        private IGGStateMachine _stateMachine;
        [SerializeField]
        private float flowerCollectRange;
        [SerializeField]
        private Vector3 centerOffset;

        public void SetStateMachine(IGGStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        

        private void Update()
        {
            var closestFlower = Flower.GetClosestFlower(transform.position+centerOffset, out var distance);
            if (distance <= flowerCollectRange)
            {
                _stateMachine.SwitchToState<FlowerEarnedState,float>(closestFlower.strength);
                closestFlower.Earn();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position+centerOffset,flowerCollectRange);
        }
    }
}
