using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stat damage;

    public int maxHealth = 100;
    public int currentHealth { get; private set; }

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(transform.name + "takes " + damage + " damage.");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.T)) {

            TakeDamage(10);
        }
    }
    public virtual void Die()
    {
        //Die 
        Debug.Log(transform.name + " died.");
        Destroy(gameObject);
    }
}
