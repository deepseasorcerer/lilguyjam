using UnityEngine;

public class TankEnemy : Enemy
{
    protected override void AIUpdate()
    {
        destination = GetDestination(5);
        state = State.Moving;
    }
}
