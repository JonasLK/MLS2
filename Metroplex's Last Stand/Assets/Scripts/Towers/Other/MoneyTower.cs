using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class MoneyTower : MonoBehaviour
{
    public MoneyManager moneyManager;
    void Awake()
    {
        moneyManager = GetComponent<OnTower>().moneyManager;
    }

    void Update()
    {
        
    }
}