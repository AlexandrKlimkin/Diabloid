using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour, IDamagable, ICameraTarget {

    public Animator Animator { get; private set; }

    public virtual float MaxHealth { get; protected set; }
    public virtual float Health { get { return _Health; } protected set { _Health = value; } }
    public float _Health;
    public Transform PointToFire { get; private set; }

    public event Action OnDamageTake;
    public event Action OnDeath;

    public bool Dead { get; private set; }

    public Vector3 LookPosition { get { return transform.position; } }

    protected virtual void Awake() {
        Animator = GetComponentInChildren<Animator>();
        PointToFire = transform.Find("PointToFire");
        if (PointToFire == null) {
            PointToFire = new GameObject("PointToFire").transform;
            PointToFire.SetParent(transform);
            PointToFire.localPosition = Vector3.up / transform.localScale.y;
            PointToFire.localRotation = Quaternion.identity;
        }
    }

    protected virtual void Start() {
        Health = 100f;
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
        Dead = true;
        if (OnDeath != null)
            OnDeath();
        if(Animator != null)
            Animator.SetTrigger("Die");

    }
}