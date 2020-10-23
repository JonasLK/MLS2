using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileTravelSpeed;
    public GameObject projectileAfterEffect;
    public int extraTurretRange;
    public Vector3 turretPosition;
    public int turretRange;

    void Update()
    {
        transform.Translate(projectileTravelSpeed * Vector3.forward * Time.deltaTime);
        if (Vector3.Distance(gameObject.transform.position, turretPosition) >= turretRange + extraTurretRange)
        {
            Destroy(gameObject);
        }
    }

    public void LookAtTarget(GameObject target)
    {
        transform.LookAt(target.transform);
    }

    public void DestroyProjectile()
    {
        if(projectileAfterEffect.GetComponent<AOEProjectile>().slow == true)
        {
            Instantiate(projectileAfterEffect, transform.position + new Vector3(0,15,0), Quaternion.identity);
        }
        else
        {
            Instantiate(projectileAfterEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}