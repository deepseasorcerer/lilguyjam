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

        [Header("Base Stats")]
        [SerializeField] private float _baseMaxHealth;
        [SerializeField] private float _baseWalkSpeed;
        [SerializeField] private float _baseRunSpeed;

        [Header("Upgrade Values")]
        [SerializeField] private float _maxHealthBonus;
        [SerializeField] private float _moveSpeedBonus;

        private float _maxHealth;
        private float _walkSpeed;
        private float _runSpeed;

        public float MaxHealth => _maxHealth;
        public float WalkSpeed => _walkSpeed;
        public float RunSpeed => _runSpeed;

        private int MaxHealthLevel;
        private int MoveSpeedLevel;

        private PlayerController controller;
        private HealthBar healthBar;

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

            controller = GetComponent<PlayerController>();
            healthBar = GetComponent<HealthBar>();
        }

        private void OnEnemyDeath(object sender, EnemyManager.OnEnemyDeathEventArgs enemy)
        {
            GainExp(enemy.Exp);
        }

        private void InitializePlayer()
        {
            Level = 1;
            MaxHealthLevel = 1;
            MoveSpeedLevel = 1;
            _walkSpeed = _baseWalkSpeed;
            _runSpeed = _baseRunSpeed;
            _maxHealth = _baseMaxHealth;
            CurrentExp = 0;
            CalculateExpToNextLevel();
        }

        #region PlayerExp
        private void CalculateExpToNextLevel()
        {
            ExpNeededToLevelUp = Level * 2.6f;
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
        #endregion

        #region PlayerUpgrades
        public void UpgradeMaxHealth()
        {
            MaxHealthLevel++;
            _maxHealth += _maxHealthBonus;
            healthBar.UpdateMaxHealth();
        }

        public void UpgradeMoveSpeed()
        {
            MoveSpeedLevel++;
            _walkSpeed += _moveSpeedBonus;
            _runSpeed += _moveSpeedBonus;
            controller.UpdateMoveSpeed();
        }
        #endregion
    }
}