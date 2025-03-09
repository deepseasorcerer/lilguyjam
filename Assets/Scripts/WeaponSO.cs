using Assets.Scripts.Debugs;
using JetBrains.Annotations;
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
    }
}