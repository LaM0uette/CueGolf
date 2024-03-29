using UnityEngine;

namespace Game.Modules.Scripts.Generics
{
    public static class MathUtilities
    {
        public static float ClampAngle(this float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
    }
}
