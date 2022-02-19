using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using GGPlugins.GGStateMachine.Scripts.Abstract;
using UnityEngine;

namespace Challenges._3._GGStateMachineCharacterPhysics.Scripts.States
{
    /// <summary>
    /// This is the most basic state with minimal overrides and no parameters.
    /// </summary>
    public class ExampleState : GGStateBase
    {
        public override void Setup()
        {
        
        }

        public override async UniTask Entry(CancellationToken cancellationToken)
        {
            Debug.Log("ExampleState: Entry");
            await UniTask.Delay(TimeSpan.FromSeconds(2f), cancellationToken: cancellationToken);
        }

        public override async UniTask Exit(CancellationToken cancellationToken)
        {
            Debug.Log("ExampleState: Exit");
        }

        public override void CleanUp()
        {
        }
    }
    
    
}
