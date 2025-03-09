using Assets.Scripts.Debugs;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class MeleeWeapon : AWeapon<WeaponSO>
    {
        public MeleeWeapon(WeaponSO info) : base(info)
        {
        }

        public override void Attack()
        {
            Debug.Log($"[MLWPN] Attacking with MeleeWeapon");
        }
    }
}