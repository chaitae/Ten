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
    SpriteRenderer ghostEye;
    SpriteRenderer ghostBody;
    SpriteRenderer ghostMouth;
    MovementMode moveMode = MovementMode.Wander;
    GameObject player;
    // Use this for initialization
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        ghostEye = transform.Find("ghostEye").GetComponent<SpriteRenderer>();
        ghostBody = transform.Find("ghostBody").GetComponent<SpriteRenderer>();
        ghostMouth = transform.Find("ghostMouth").GetComponent<SpriteRenderer>();
        ChooseRandomLocation();
        agent.speed = speed;
    }
    public void SetFace(Sprite eye, Sprite body,Sprite mouth)
    {
        ghostEye.sprite = eye;
        ghostBody.sprite = body;
        ghostMouth.sprite = mouth;
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
            }
        }
        else if(moveMode == MovementMode.Stop)
        {
            agent.isStopped = true;
            agent.ResetPath();
        }
    }
    public void interact()
    {
        if(isImposter)
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
