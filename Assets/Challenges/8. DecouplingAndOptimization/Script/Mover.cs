using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Challenges._8._DecouplingAndOptimization.Script
{
    public class Mover : MonoBehaviour
    {
        public List<Vector3> positions = new List<Vector3>();
        private List<Vector3> worldPositions = new List<Vector3>();

        private void Start()
        {
            worldPositions.Add(transform.position);
            foreach (var pos in positions)
            {
                worldPositions.Add(transform.TransformPoint(pos));
            }
            MovementLoop(gameObject.GetCancellationTokenOnDestroy());
        }

        async UniTask MovementLoop(CancellationToken token)
        {
            int nextIndex = 1;
            while (true)
            {
                var pos = worldPositions[nextIndex];
                var cancelled = await transform.DOMove(pos, (pos - transform.position).magnitude/5f).AsyncWaitForCompletion().AsUniTask().AttachExternalCancellation(token).SuppressCancellationThrow();
                if (cancelled) break;
                nextIndex++;
                if (nextIndex >= positions.Count) nextIndex = 0;
            }
        }

        private void OnDrawGizmos()
        {
            var instanceId = gameObject.GetInstanceID();
            Gizmos.color = new Color((instanceId % 255) / 255f, (instanceId*(28573) % 255) / 255f, (instanceId*(7652) % 255) / 255f);
            if (worldPositions.Count == positions.Count+1)
            {
                for (int i = 1; i < worldPositions.Count; i++)
                {
                    var prevPos = worldPositions[i - 1];
                    var pos = worldPositions[i];
                    Gizmos.DrawLine(prevPos,pos);
                    Gizmos.DrawCube(pos,Vector3.one*0.3f);
                }

                return;
            }
            for (int i = 0; i < positions.Count; i++)
            {
                var prevPos = i == 0 ? transform.position : transform.TransformPoint(positions[i - 1]);
                var pos =  transform.TransformPoint(positions[i]);
                Gizmos.DrawLine(prevPos,pos);
                Gizmos.DrawCube(pos,Vector3.one*0.3f);
            }
        }
    }
}
