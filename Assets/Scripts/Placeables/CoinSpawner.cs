using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public float MoneyToAdd;
    MoneyManager moneyManager;
    DragNDrop drop;

    private void Awake()
    {
        moneyManager = FindFirstObjectByType<MoneyManager>();
        drop = GetComponent<DragNDrop>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Unit"))
        {
            AddMoney();
        }
    }

    void AddMoney()
    {
        if (drop.bought && !drop.held)
        {
            moneyManager.MoneyIN(MoneyToAdd);
        }
    }
}
