using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public Vector3 destination;

    public enum State
    {
        Idle,
        Moving
    }
    public State state;

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
