using System.Collections.Generic;
using UnityEngine;

namespace Challenges._3._GGStateMachineCharacterPhysics.Scripts.MonoBehaviours
{
    public class Flower : MonoBehaviour
    {
        private static List<Flower> _activeFlowers = new List<Flower>();
        public static Flower GetClosestFlower(Vector3 position, out float distance)
        {
            if (_activeFlowers.Count == 0)
            {
                distance = 0;
                return null;
            }
            float dist = float.MaxValue;
            Flower closest = null;
            foreach (var flower in _activeFlowers)
            {
                var flowerDist = Vector3.Distance(flower.transform.position, position);
                if (flowerDist < dist)
                {
                    dist = flowerDist;
                    closest = flower;
                }
            }

            distance = dist;
            return closest;
        }
            
            
        public float strength;

        private void Start()
        {
            _activeFlowers.Add(this);
        }

        public void Earn()
        {
            _activeFlowers.Remove(this);
            Destroy(gameObject);
        }
    }
}
