using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour, IDamagable {

    public Animator Animator { get; private set; }

    public virtual float MaxHealth { get; protected set; }
    public virtual float Health { get; protected set; }

    public event Action OnDamageTake;
    public event Action OnDeath;

    public bool Dead { get; private set; }

    protected virtual void Awake() {
        Animator = GetComponentInChildren<Animator>();
    }

    protected virtual void Start() {
        Health = 1f;
    }

    public virtual void TakeDamage(Damage damage) {
        if (Dead)
            return;
        Health -= damage.Amount;
        if(OnDamageTake != null)
            OnDamageTake();
        if (Health <= 0) {
            Health = 0;
            Die();
        }
    }

    public virtual void Die() {
        if (OnDeath != null)
            OnDeath();
        if(Animator != null)
            Animator.SetBool("Dead", true);
    }
}