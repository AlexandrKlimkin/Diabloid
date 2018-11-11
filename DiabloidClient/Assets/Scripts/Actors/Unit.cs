using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : Actor {
    public MoveController MoveController { get; private set; }
    public AttackController AttackController { get; private set; }
    public Collider Collider { get; private set; }

    protected override void Awake() {
        base.Awake();
        MoveController = GetComponent<MoveController>();
        AttackController = GetComponentInChildren<AttackController>();
        Collider = GetComponent<Collider>();
    }
    
    public override void Die() {
        base.Die();
        Collider.enabled = false;
    }
}
