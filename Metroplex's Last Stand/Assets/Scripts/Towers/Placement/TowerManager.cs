using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public bool placing;
    public Vector3 placePos;
    public Vector3 nodeOffset;
    public GameObject blueprint;
    public GameObject lastPlacedTower;
    public GameObject lastHoveredNode;
    public int towerToPlace;
    public List<GameObject> preTowers = new List<GameObject>();
    public List<GameObject> towers = new List<GameObject>();
    public MoneyManager moneyManager;
    public Manager manager;

    public void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
        moneyManager = manager.money;
    }

    public void Update()
    {
        if (placing == true && Input.GetMouseButtonDown(0))
        {
            if (moneyManager.money >= towers[towerToPlace].GetComponent<OnTower>().cost)
            {
                if (lastHoveredNode.GetComponent<BuildingCheck>().occupied == false)
                {
                    Destroy(blueprint);
                    lastPlacedTower = Instantiate(towers[towerToPlace], placePos + nodeOffset, Quaternion.identity);
                    moneyManager.money -= towers[towerToPlace].GetComponent<OnTower>().cost;
                    moneyManager.UpdateMoneyDisplay();
                    moneyManager.buyPriceDisplayGo.SetActive(false);
                    placing = false;
                }
            }
            else
            {
                Destroy(blueprint);
            }
        }

        if (placing == true && Input.GetMouseButtonDown(1))
        {
            placing = false;
            moneyManager.buyPriceDisplayGo.SetActive(false);
            Destroy(blueprint);
        }
    }

    public void FakeStart()
    {
        moneyManager = manager.money;
    }

    public void BlueprintTower(int newTowerToPlace)
    {
        placing = true;
        towerToPlace = newTowerToPlace;
        Instantiate(preTowers[towerToPlace], placePos + nodeOffset, Quaternion.identity);
        moneyManager.CostPriceUpdate(towers[towerToPlace].GetComponent<OnTower>().cost);
        blueprint = GameObject.FindGameObjectWithTag("PreTower");
        moneyManager.CostPriceUpdate(towers[towerToPlace].GetComponent<OnTower>().cost);
        moneyManager.buyPriceDisplayGo.SetActive(true);
    }

    public void ChangeBluePrintPosition(Vector3 newPos)
    {
        placePos = newPos;
        if (placing == true && blueprint != null)
        {
            blueprint.transform.position = newPos + nodeOffset;
        }
    }
}