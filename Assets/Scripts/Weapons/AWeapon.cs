using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public abstract class AWeapon<T> : IWeapon
        where T : WeaponSO
    {
        protected AWeapon(T info)
        {
            Info = info;
        }

        public T Info { set; get; }
        public WeaponSO BaseInfo => Info;

        public virtual void Attack()
        {
            Debug.Log($"[AWPN] Attacking");
        }
    }
}