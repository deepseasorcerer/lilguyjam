using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float MaxHealth;
    public Transform target;
    public Vector3 destination;
    public State state;
    public enum State
    {
        Idle,
        Moving
    }

    private float currentHealth;

    private void Awake()
    {
        currentHealth = MaxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        CheckIfDead();
    }

    private void CheckIfDead()
    {
        if (currentHealth > 0)
            return;

        EnemyDied();
    }

    //Handle everything related to the death of an enemy
    private void EnemyDied()
    {
        Debug.Log($"Enemy died");
        gameObject.SetActive(false);
    }

    protected Vector3 GetDestination(int distance)
    {
        Vector3 vector = transform.position - target.position;
        return target.position + (vector.normalized * distance);
    }

    private void Update() {
        AIUpdate();
        if (state.Equals(State.Moving))
        {
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.destination = destination; 
        }
    }

    protected virtual void AIUpdate()
    {

    }
}
