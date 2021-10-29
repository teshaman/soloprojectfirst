using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    GameObject gameManager;
    GameObject uiManager;

    public int maxHealth = 100;
    public int maxHealthIncreasePerWave = 10;
    public int currentHealth = 100;
    public int ticketAmount = 1;

    public float timeTillDeath = 1f;
    public bool isDeath = false;

    private void Awake()
    {
        currentHealth = maxHealth;
        gameManager = GameObject.FindWithTag("GameManager");
        //uiManager = GameObject.FindWithTag("UIManager"); // TODO: UI
    }

    private void Update()
    {
    }

    public void Damage(int damage)
    {
        currentHealth = currentHealth - damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (gameObject.tag == "Enemy")
        {
            if (isDeath == false)
            {
                //uiManager.GetComponent<MenuSystem>().levelCompleted = true;
            }

            gameManager.GetComponent<TicketSystem>().IncrementTicketCount(ticketAmount);
            EnemyDeath();
            isDeath = true;
        }
    }

    public void EnemyDeath()
    {
        Destroy(gameObject);
    }
}
