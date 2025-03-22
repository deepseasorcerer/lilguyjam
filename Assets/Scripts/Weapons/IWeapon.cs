namespace Assets.Scripts.Weapons
{
    public interface IWeapon
    {
        public WeaponSO BaseInfo { get; }
        public void Attack();
        public void Upgrade();
    }
}