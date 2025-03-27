using System;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class EnemyManager : MonoBehaviour
    {
        public static EnemyManager Instance { get; private set; }
        public event EventHandler<OnEnemyDeathEventArgs> OnEnemyDeath;

        public class OnEnemyDeathEventArgs : EventArgs
        {
            public float Exp;
        }

        private void Awake()
        {
            if (Instance != null)
                Destroy(gameObject);
            else
                Instance = this;
        }

        public void TriggerOnEnemyDeath(float exp)
        {
            OnEnemyDeath?.Invoke(this, new OnEnemyDeathEventArgs
            {
                Exp = exp
            });
        }
    }
}