using Assets.Scripts.Enemies;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        public static Player Instance { get; private set; }

        public int Level { get; private set; }
        public float CurrentExp { get; private set; }
        public float ExpNeededToLevelUp { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            InitializePlayer();
        }

        private void Start()
        {
            EnemyManager.Instance.OnEnemyDeath += OnEnemyDeath;
        }

        private void InitializePlayer()
        {
            Level = 1;
            CurrentExp = 0;
            CalculateExpToNextLevel();
        }

        private void CalculateExpToNextLevel()
        {
            ExpNeededToLevelUp = Level * 2.6f;
        }

        private void OnEnemyDeath(object sender, EnemyManager.OnEnemyDeathEventArgs enemy)
        {
            GainExp(enemy.Exp);
        }

        private void GainExp(float amount)
        {
            CurrentExp += amount;
            CanLevelUp();
        }

        private void CanLevelUp()
        {
            if (CurrentExp < ExpNeededToLevelUp)
                return;

            LevelUp();
        }

        private void LevelUp()
        {
            Level++;

            ResetCurrentExp();
            CalculateExpToNextLevel();
            CanLevelUp();
        }

        private void ResetCurrentExp() => CurrentExp = GetExpOverhead();
        private float GetExpOverhead() => CurrentExp - ExpNeededToLevelUp;
    }
}