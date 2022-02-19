using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using GGPlugins.GGStateMachine.Scripts.Abstract;
using UnityEngine;

namespace Challenges._3._GGStateMachineCharacterPhysics.Scripts.States
{
    /// <summary>
    /// This state takes a float as input, a state can take up to 4 parameters
    /// </summary>
    public class ExampleParametrizedState : GGStateBase<float>
    {
        private float _staticWaitTime;
        private float _dynamicWaitTime;

        /// <summary>
        /// The parameters here will only be given during creation
        /// </summary>
        /// <param name="staticWaitTime"></param>
        public ExampleParametrizedState(float staticWaitTime)
        {
            _staticWaitTime = staticWaitTime;
        }
        
        /// <summary>
        /// The parameters here are given when the machine is requested to switch to this state
        /// </summary>
        /// <param name="param"></param>
        public override void Setup(float param)
        {
            _dynamicWaitTime = param;
        }

        public override async UniTask Entry(CancellationToken cancellationToken)
        {
            Debug.Log("ExampleParametrizedState: Entry");
            await UniTask.Delay(TimeSpan.FromSeconds(_staticWaitTime+_dynamicWaitTime), cancellationToken: cancellationToken);
        }

        public override async UniTask Exit(CancellationToken cancellationToken)
        {
            Debug.Log("ExampleParametrizedState: Exit");
        }

        public override void CleanUp()
        {
        }

        #region The following overrides are optional and independent of parameter count

        /// <summary>
        /// Called when the machine starts
        /// </summary>
        public override void OnMachineStarted()
        {
            base.OnMachineStarted();
        }

        /// <summary>
        /// Called when the machine starts and this is the first state
        /// </summary>
        public override void OnMachineStartState()
        {
            base.OnMachineStartState();
        }

        /// <summary>
        /// Called after all the states have exit & cleaned up and the machine is exiting
        /// </summary>
        public override void OnMachineExit()
        {
            base.OnMachineExit();
        }

        #endregion
    }
}
