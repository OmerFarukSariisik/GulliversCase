using UnityEngine;

namespace Challenges._3._GGStateMachineCharacterPhysics.Scripts
{
    public static class Vector2Extension 
    {
        public static Vector3 ToXZPlane(this Vector2 vector2)
        {
            return new Vector3(vector2.x, 0, vector2.y);
        }
    }
}
