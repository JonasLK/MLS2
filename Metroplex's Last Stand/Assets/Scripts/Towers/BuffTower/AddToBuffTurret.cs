using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToBuffTurret : MonoBehaviour
{
    public string buffTurretName;
    private int buffTowersInRange;
    private GameObject buffTowerParticle;

    public void OnTriggerEnter(Collider o)
    {
        if(o.gameObject.tag == buffTurretName && gameObject.tag != buffTurretName)
        {
            o.gameObject.GetComponent<BuffTowers>().turretsInRange.Add(gameObject);
            buffTowersInRange += 1;
            if(GetComponent<Shooting>().Buffed == false)
            {
                GetComponent<Shooting>().BuffUp(o.gameObject.GetComponent<BuffTowers>().attackSpeedBuff, o.gameObject.GetComponent<BuffTowers>().attackDamageBuff);
                buffTowerParticle = Instantiate(o.gameObject.GetComponent<BuffTowers>().buffParticle, gameObject.transform.position, Quaternion.identity);
            }
        }
    }

    public void OnTriggerExit(Collider o)
    {
        if (o.gameObject.tag == buffTurretName && gameObject.tag != buffTurretName)
        {
            o.gameObject.GetComponent<BuffTowers>().turretsInRange.Remove(gameObject);
            buffTowersInRange -= 1;
            if(buffTowersInRange == 0)
            {
                GetComponent<Shooting>().Buffed = false;
                GetComponent<Shooting>().BuffUp(0,0);
                Destroy(buffTowerParticle);
            }
        }
    }
}