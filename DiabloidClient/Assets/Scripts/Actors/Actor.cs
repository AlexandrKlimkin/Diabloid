using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour, IDamagable {

    public virtual float MaxHealth { get; protected set; }
    public virtual float Health { get; protected set; }

    public event Action OnDamageTake;
    public event Action OnDeath;

    public virtual bool Dead {
        get {
            return Health <= 0; 
        }
    }

    protected virtual void Awake() {

    }

    public virtual void TakeDamage(Damage damage) {
        if (Dead)
            return;
        Health -= damage.Amount;
        OnDamageTake();
        if (Dead)
            Die();
    }

    public virtual void Die() {
        if (OnDeath != null)
            OnDeath();
    }
}