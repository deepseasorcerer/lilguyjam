using Assets.Scripts.Debugs;
using Assets.Scripts.Enemies;
using System.Linq;
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
            Transform playerTransform = Player.Player.Instance.transform;
            Vector3 startPos = playerTransform.position + playerTransform.forward * BaseInfo.AttackOffset;

            float range = BaseInfo.AttackRange;
            float damage = BaseInfo.Damage;
            float attackAngle = BaseInfo.AttackAngle;
            int rayCount = BaseInfo.RayCount;

            float angleStep = attackAngle / (rayCount - 1);
            float halfAngle = attackAngle / 2f;

            Player.Player.Instance.TryGetComponent(out MeleeWeaponDebug debugger);

            RaycastHit[] hits = new RaycastHit[] { };

            for (int i = 0; i < rayCount; i++)
            {
                float currentAngle = -halfAngle + i * angleStep;
                Quaternion rotation = Quaternion.AngleAxis(currentAngle, Vector3.up);
                Vector3 rayDirection = rotation * playerTransform.forward;

                if (debugger != null) //Debug
                {
                    debugger.DrawRayDebug(startPos, rayDirection, range);
                }

                hits = Physics.RaycastAll(startPos, rayDirection, range);
            }

            RaycastHit[] uniqueHits = hits.GroupBy(hit => hit.colliderInstanceID).Select(group => group.First()).ToArray();

            foreach (RaycastHit hit in uniqueHits)
            {
                if (hit.collider.gameObject.TryGetComponent(out EnemyHealth enemy))
                {
                    enemy.TakeDamage(damage);
                }
            }
        }
    }
}