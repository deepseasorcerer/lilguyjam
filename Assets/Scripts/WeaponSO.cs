using Assets.Scripts.Debugs;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "WeaponInfo", menuName = "Weapon")]
    public class WeaponSO : ScriptableObject
    {
        public GameObject WeaponModel;

        [Header("WeaponData")]
        public float Damage;
        [Tooltip("Attacks per second")]
        public float AttackSpeed;
        [Tooltip("The range of the attack. How far out can it hit")]
        public float AttackRange;
        [Tooltip("Hwo wide the sword attack is")]
        public float AttackAngle = 45f;
        [Tooltip("How many rays are used to detect collision. Base is 25")]
        public int RayCount = 25;
        [Tooltip("How far away from the player should the attack start, base is 0.5")]
        public float AttackOffset = 0.5f;
    }
}