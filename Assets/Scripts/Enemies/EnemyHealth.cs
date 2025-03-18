using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class EnemyHealth : MonoBehaviour
    {
        public float MaxHealth;

        private float _currentHealth;

        private void Awake()
        {
            _currentHealth = MaxHealth;
        }

        public void TakeDamage(float amount)
        {
            _currentHealth -= amount;

            CheckIfDead();
        }

        private void CheckIfDead()
        {
            if (_currentHealth > 0)
                return;

            EnemyDied();
        }

        //Handle everything related to the death of an enemy
        private void EnemyDied()
        {
            Debug.Log($"Enemy died");
            gameObject.SetActive(false);
        }
    }
}