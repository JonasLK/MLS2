using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEProjectile : MonoBehaviour
{
    public bool slow;
    public float slowamount;
    public float explosionDamage;
    public float lifeTime;
    public void Start()
    {
        Despawn();
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}