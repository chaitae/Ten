using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MovementMode
{
    Wander,Flee,Stop
}

public class Ghost : MonoBehaviour,IInteractable {
    NavMeshAgent agent;
    public bool isImposter = false;
    public float range = 10.0f;
    public float sightDistance;
    public float speed = 2f;
    float distanceFromPlayer;
    MovementMode moveMode = MovementMode.Wander;
    GameObject player;
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        ChooseRandomLocation();
        agent.speed = speed;
    }
    void ChooseRandomLocation()
    {
            Vector3 point;
            while (!RandomPoint(transform.position, range, out point))
            {

            }
            Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
            agent.SetDestination(point);
    }
    void RunAway()
    {
        Vector3 dirToPlayer = transform.position - player.transform.position;
        Vector3 newPos = transform.position + dirToPlayer;
        agent.SetDestination(newPos);
    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        result = Vector3.zero;
        return false;
    }

    void Update ()
    {
        distanceFromPlayer = Vector3.Distance(this.transform.position, player.transform.position);

        //movement flee action
        if(moveMode == MovementMode.Flee)
        {
            if (!agent.hasPath)
            {
                RunAway();
            }
            if(distanceFromPlayer > sightDistance)
            {
                moveMode = MovementMode.Wander;
                agent.isStopped = true;
                agent.ResetPath();
            }
        }
        else if (moveMode == MovementMode.Wander)
        {
            if (!agent.hasPath)
            {
                ChooseRandomLocation();
            }
            if (distanceFromPlayer <= sightDistance)
            {
                moveMode = MovementMode.Flee;
                agent.isStopped = true;
                agent.ResetPath();
                Debug.Log("ruuuun");
            }
        }
        else if(moveMode == MovementMode.Stop)
        {
            agent.isStopped = true;
            agent.ResetPath();
        }
        //movement wander action
        
    }

    public void interact()
    {
        VacuumedAction();
    }

    public void interact(KeyCode key)
    {
    }
    public void VacuumedAction()
    {
        VacuumCollisionHandler vacuumCol = GameObject.FindObjectOfType<VacuumCollisionHandler>();
        this.transform.SetParent(vacuumCol.gameObject.transform);
        moveMode = MovementMode.Stop;
        Debug.Log(this.name + " vacuumed");
        StartCoroutine(WaitAndDestroy(3));
    }

    private IEnumerator WaitAndDestroy(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}
