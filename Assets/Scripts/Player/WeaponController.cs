using Assets.Scripts.Weapons;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private Transform _weaponPosition;
        [SerializeField] private WeaponSO _startWeapon;

        private GameObject _weaponModelInstance;
        private IWeapon _currentWeapon;
        public IWeapon CurrentWeapon
        {
            set
            {
                if (_weaponModelInstance != null)
                {
                    Destroy(_weaponModelInstance);
                }
                _currentWeapon = value;

                if (_currentWeapon != null)
                {
                    _weaponModelInstance = Instantiate(_currentWeapon.BaseInfo.WeaponModel, _weaponPosition);
                    _weaponModelInstance.transform.localPosition = Vector3.zero;
                }
            }
            get => _currentWeapon;
        }

        private bool _canAttack = true;

        private void Start()
        {
            CurrentWeapon = new MeleeWeapon(_startWeapon);
        }

        public void Attack()
        {
            if (_currentWeapon == null || !_canAttack)
                return;

            _canAttack = false;
            _currentWeapon.Attack();

            StartCoroutine(StartAttackCooldown());
        }

        private IEnumerator StartAttackCooldown()
        {
            yield return new WaitForSeconds(1f / _currentWeapon.BaseInfo.AttackSpeed);
            _canAttack = true;
        }

        public void UpgradeWeaponDamage()
        {
            if (!Player.Instance.CanSpendUpgradePoint())
                return;

            CurrentWeapon.Upgrade();
        }
    }
}