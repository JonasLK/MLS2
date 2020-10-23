using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddEnemyToTargeting : MonoBehaviour
{
    public string nameTurretTag = "Turret";
    public string nameAOEProjectileTag = "AOEProjectile";
    public int maxSlows = 1;
    private int amountSlowFieldsEntered;

    public void OnTriggerEnter(Collider o)
    {
        if (o.gameObject.tag == nameTurretTag)
        {
            o.gameObject.GetComponent<Targeting>().enemiesInRange.Add(gameObject);
        }

        if (o.gameObject.tag == nameAOEProjectileTag)
        {
            if(o.gameObject.GetComponent<AOEProjectile>().slow == true)
            {
                if(amountSlowFieldsEntered < maxSlows)
                {
                    GetComponent<Enemy>().Slow(o.gameObject.GetComponent<AOEProjectile>().slowamount);
                    amountSlowFieldsEntered += 1;
                }
            }
            else
            {
                GetComponent<Enemy>().Damage(o.gameObject.GetComponent<AOEProjectile>().explosionDamage);
            }
        }

        if(o.gameObject.tag == "Projectile")
        {
            o.gameObject.GetComponent<Projectile>().DestroyProjectile();
        }
    }

    public void OnTriggerExit(Collider o)
    {
        if (o.gameObject.tag == nameTurretTag)
        {
            o.gameObject.GetComponent<Targeting>().enemiesInRange.Remove(gameObject);
            if(gameObject == o.gameObject.GetComponent<Targeting>().currentTarget)
            {
                o.gameObject.GetComponent<Targeting>().currentTarget = null;
            }
        }

        if (o.gameObject.tag == nameAOEProjectileTag)
        {
            if (o.gameObject.GetComponent<AOEProjectile>().slow == true)
            {
                amountSlowFieldsEntered -= 1;
                if(amountSlowFieldsEntered == 0)
                {
                    GetComponent<Enemy>().Slow(0);
                }
            }
        }
    }
}