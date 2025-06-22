using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharpenedBlades : MonoBehaviour
{
    UnitInteractions interactions;
    [SerializeField] int damageIncrease;

    private void Awake()
    {
        interactions = FindFirstObjectByType<UnitInteractions>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Unit"))
        {
            interactions.Damage += damageIncrease;
        }
    }
}
