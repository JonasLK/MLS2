using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float baseSpeed = 10f;
    private float actualSpeed;

    public float health = 100;

    private Transform target;
    private int wavepointIndex = 0;

    private void Start()
    {
        target = Waypoints.points[0];
        actualSpeed = baseSpeed;
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * actualSpeed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }

    public void Damage(float damage)
    {
        health -= damage;
        Death();
    }

    public void Slow(float slowAmount)
    {
        if(slowAmount == 0)
        {
            actualSpeed = baseSpeed;
        }
        else
        {
            actualSpeed -= actualSpeed * slowAmount;
        }
    }

    public void Death()
    {
        if(health <= 0)
        {

            Destroy(gameObject);
        }
    }
}