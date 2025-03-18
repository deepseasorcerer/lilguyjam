using UnityEngine;

public class SmallRatEnemy : Enemy
{
    protected override void AIUpdate()
    {
        destination = GetDestination(5);
        state = State.Moving;
    }
}