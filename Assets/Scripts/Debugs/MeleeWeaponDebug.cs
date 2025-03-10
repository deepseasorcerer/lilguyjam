using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Assets.Scripts.Debugs
{
    public class MeleeWeaponDebug : MonoBehaviour
    {
        public void DrawRayDebug(Vector3 startPos, Vector3 rayDirection, float range)
        {
            Debug.DrawRay(startPos, rayDirection * range, Color.yellow, 2f);
        }
    }
}