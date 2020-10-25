using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public Manager manager;
    public TowerManager towerManager;
    public Text moneyDisplay;
    public Text buyPriceDisplay;
    public GameObject buyPriceDisplayGo;
    public float money;
    public int buyPrice;

    public void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
        towerManager = GameObject.FindGameObjectWithTag("TowerManager").GetComponent<TowerManager>();
        moneyDisplay.text = "Money: $" + money.ToString();
    }

    public void UpdateMoneyDisplay()
    {
        moneyDisplay.text = "Money: $" + money.ToString();
    }
    public void CostPriceUpdate(int towerCost)
    {
        buyPrice = towerCost;
        buyPriceDisplay.text = "buy: $" + buyPrice.ToString();
    }
}