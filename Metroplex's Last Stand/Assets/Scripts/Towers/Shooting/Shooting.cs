﻿using DigitalRuby.LightningBolt;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public bool tesla;
    public bool money;
    private float shotTimer;
    public float baseFireRate;
    private float actualFireRate;
    public float baseDamage;
    private float actualDamage;
    public float moneyPerTick;
    public GameObject projectile;
    public GameObject teslaProjectile;
    private GameObject justSpawnedTeslaProjectile;
    public GameObject moneyProjectile;
    private GameObject justSpawnedMoneyProjectile;
    private GameObject firedProjectile;
    public float projectileTravelSpeed;
    public Targeting targeting;
    public Transform shotPoint;
    public bool Buffed;
    private MoneyManager moneyManager;

    void Awake()
    {
        actualFireRate = baseFireRate;
        if(money == false)
        {
            targeting = GetComponent<Targeting>();
        }
        if (tesla == true)
        {
            targeting.tesla = true;
        }
        if(projectile != null)
        {
            projectile.GetComponent<Projectile>().projectileTravelSpeed = projectileTravelSpeed;
        }
        if(money == true)
        {
            //moneyManager = GameObject.FindGameObjectWithTag("MoneyManager").GetComponent<MoneyManager>();
        }
    }

    void Update()
    {
        shotTimer += Time.deltaTime;
        if(tesla == false && money == false)
        {
            if(shotTimer >= actualFireRate && targeting.currentTarget != null)
            {
                firedProjectile = Instantiate(projectile, shotPoint.position, Quaternion.identity);
                firedProjectile.GetComponent<Projectile>().LookAtTarget(targeting.currentTarget);
                firedProjectile.GetComponent<Projectile>().turretPosition = shotPoint.position;
                firedProjectile.GetComponent<Projectile>().turretRange = GetComponent<Targeting>().Range;
               shotTimer = 0;
            }
        }
        
        if(tesla == true)
        {
            if (shotTimer >= actualFireRate && targeting.enemiesInRange.Count > 0)
            {
                foreach (GameObject enemy in targeting.enemiesInRange)
                {
                    justSpawnedTeslaProjectile = Instantiate(teslaProjectile, shotPoint.transform.position, Quaternion.identity);
                    justSpawnedTeslaProjectile.GetComponent<LightningBoltScript>().StartObject = shotPoint.gameObject;
                    justSpawnedTeslaProjectile.GetComponent<LightningBoltScript>().EndObject = enemy;
                    StartCoroutine(DespawnLighting(justSpawnedTeslaProjectile));
                    //play sound
                    enemy.GetComponent<Enemy>().Damage(actualDamage);
                }
                    shotTimer = 0;
            }
        }

        if(money == true)
        {
            if (shotTimer >= actualFireRate)
            {
                justSpawnedMoneyProjectile = Instantiate(moneyProjectile, transform.position, Quaternion.identity);
                justSpawnedMoneyProjectile.GetComponentInChildren<ParticleSystem>().emission.SetBurst(0, new ParticleSystem.Burst(0,moneyPerTick));
                StartCoroutine(DespawnLighting(justSpawnedMoneyProjectile));
                //moneyManager.money += moneyPerTick;
                //moneyManager.UpdateMoneyDisplay();
                //play sound
                shotTimer = 0;
            }
        }
    }

    IEnumerator DespawnLighting(GameObject particleToDespawn)
    {
        yield return new WaitForSeconds(actualFireRate/2);
        Destroy(particleToDespawn);
    }

    public void BuffUp(float attackSpeedBuff, float attackDamageBuff)
    {
        if(attackSpeedBuff == 0)
        {
            actualFireRate = baseFireRate;
        }
        else
        {
            actualFireRate -= actualFireRate * attackSpeedBuff;
        }
    }
}