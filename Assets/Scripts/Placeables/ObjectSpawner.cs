using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject ObjectToSpawn;

    [Header("Coins")]
    public bool Coin;
    int coinCount = 0;
    public int MaxCoins;

    private void OnTriggerExit(Collider other)
    {
        if (Coin)
        {
            if (coinCount == MaxCoins - 1) 
            {
                GameObject coin = Instantiate(ObjectToSpawn, transform);
                coin.transform.position = transform.position;
                coin.GetComponent<Collider>().enabled = false;
                print("To many Coins");
                return; 
            } else { coinCount++; }
        } 

        GameObject o = Instantiate(ObjectToSpawn, transform);
        o.transform.position = transform.position;
    }
}
