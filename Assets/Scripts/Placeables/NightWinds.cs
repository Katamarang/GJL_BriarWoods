using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightWinds : MonoBehaviour
{
    UnitInteractions interactions;
    [SerializeField] int damage;

    private void Awake()
    {
        interactions = FindFirstObjectByType<UnitInteractions>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Unit"))
        {
            interactions.TakeDamge(damage);
        }
    }
}
