using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Castle : MonoBehaviour
{
    public bool playerInSpace;

    [SerializeField] float MaxHealth;
    float health;

    [SerializeField] int damage;
    [SerializeField] float timeBetweenAttacks;

    public bool PlayerCastle;

    public Slider slider;


    private void OnEnable() { UnitController.DestinationReached += AtDesination; }
    private void OnDisable() { UnitController.DestinationReached -= AtDesination; }

    private void OnTriggerEnter(Collider other) { if (other.CompareTag("Unit")) { playerInSpace = true; } }
    private void OnTriggerExit(Collider other) { playerInSpace = false; }

    private void Awake()
    {
        slider.maxValue = MaxHealth;
        health = MaxHealth;
    }

    void AtDesination(UnitController unit)
    {
        if (playerInSpace)
        {
            InvokeRepeating("AttackPlayer", timeBetweenAttacks, timeBetweenAttacks);
        }
    }

    private void Update()
    {
        slider.value = health;
    }

    void AttackPlayer()
    {
        UnitInteractions unit = FindObjectOfType<UnitInteractions>();
        if (unit != null && playerInSpace)
        {
            unit.TakeDamge(damage);
        }
    }

    public void TakeDamge(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            print("castle destroyed");
            if (PlayerCastle)
            {
                SceneManager.LoadScene("Win");
            } else
            {
                SceneManager.LoadScene("Lose");
            }
        }
    }

    public void StopAttacking()
    {
        CancelInvoke("AttackPlayer");
        playerInSpace = false;
    }
}
