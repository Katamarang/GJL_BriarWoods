using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowRations : MonoBehaviour
{
    UnitInteractions interactions;
    UnitController controller;
    [SerializeField] int damage;

    private void Awake()
    {
        interactions = FindFirstObjectByType<UnitInteractions>();
        controller = FindFirstObjectByType<UnitController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Unit"))
        {
            interactions.TakeDamge(damage);
            controller.Speed = 0.7f;
        }
    }
}
