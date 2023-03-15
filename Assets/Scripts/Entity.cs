using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Allegiance
{
    town,
    invader
}

public abstract class Entity : MonoBehaviour
{
    [Header("Entity Stats")]
    [SerializeField] protected int health;
    [SerializeField] protected int maxHealth;
    [SerializeField] protected Allegiance team;
    
    public int Health { get => health; }
    public int MaxHealth { get => maxHealth; }
    public Allegiance Team { get => team; }

    public virtual void ReceiveHit(int damage)
    {
        health = Mathf.Clamp(health - damage, 0, maxHealth);

        if (health <= 0)
        {
            Invoke(nameof(Die), 0.05f);
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
