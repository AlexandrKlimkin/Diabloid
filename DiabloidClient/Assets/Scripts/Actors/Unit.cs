using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : Actor {
    public Rigidbody Rigidbody { get; private set; }
    public MoveController MoveController { get; private set; }
    public AttackController AttackController { get; private set; }
    public SphereCollider SelectCollider { get; private set; }
    public Collider Collider { get; private set; }

    protected override void Awake() {
        base.Awake();
        MoveController = GetComponent<MoveController>();
        AttackController = GetComponentInChildren<AttackController>();
        Rigidbody = GetComponent<Rigidbody>();
        SelectCollider = GetComponentInChildren<SphereCollider>();
        Collider = GetComponent<Collider>();
    }
    
    public override void Die() {
        base.Die();
        if(SelectCollider != null)
            SelectCollider.enabled = false;
        if (Collider != null)
            Collider.enabled = false;
        if (Rigidbody != null) {
            Rigidbody.detectCollisions = false;
            Rigidbody.isKinematic = true;
        }
        AttackController.enabled = false;
        MoveController.enabled = false;
    }

    public override void TakeDamage(Damage damage) {
        base.TakeDamage(damage);
        if (!Dead) {
            if (Animator != null) {
                if (damage.Type == DamageType.Middle) {
                    Animator.SetTrigger("TakeMiddleDamage");
                    MoveController.StanOnTime(1f);
                } else if(damage.Type == DamageType.Big) {
                    Animator.SetTrigger("TakeBigDamage");
                    MoveController.StanOnTime(2.5f);
                }
            }
        }
    }
}