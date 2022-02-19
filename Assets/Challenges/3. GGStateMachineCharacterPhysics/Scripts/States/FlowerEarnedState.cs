using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GGPlugins.GGStateMachine.Scripts.Abstract;
using UnityEngine;

namespace Challenges._3._GGStateMachineCharacterPhysics.Scripts.States
{
    /// <summary>
    /// DO NOT EDIT THIS (unless you really want to improve it)
    /// </summary>
    public class FlowerEarnedState : GGStateBase<float>
    {
        private readonly Transform _characterTransform;
        private readonly Transform _headTransform;
        private float _strength;
        private float _time;

        public FlowerEarnedState(Transform characterTransform, Transform headTransform)
        {
            _characterTransform = characterTransform;
            _headTransform = headTransform;
        }
        public override void Setup(float strength)
        {
            _strength = strength;
            _time = strength*2f;
        }

        public override async UniTask Entry(CancellationToken cancellationToken)
        {
            _characterTransform.DOJump(_characterTransform.position, _strength * 4, 1, _time);
            _headTransform.DOPunchScale(Vector3.one * _strength, _time, 7);
            await _characterTransform.DOPunchScale(Vector3.one * _strength, _time, 7).AsyncWaitForCompletion();
            StateMachine.SwitchToState<IdleState>();
        }

        public override async UniTask Exit(CancellationToken cancellationToken)
        {
        }

        public override void CleanUp()
        {
        }
    }
}
