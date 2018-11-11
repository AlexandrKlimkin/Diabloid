using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SingletonBehaviour<PlayerController> {

    public Unit Owner { get; private set; }
    public MoveController MoveController { get; private set; }
    public AttackController AttackController { get; private set; }

    private InputSystem _Input;

    protected override void Awake () {
        base.Awake();
        Owner = GetComponent<Unit>();
        MoveController = GetComponent<MoveController>();
    }
	
    protected void Start() {
        AttackController = Owner.AttackController;
        _Input = InputSystem.Instance;
        _Input.TouchGroundHit += MoveToGroundHitPoint;
        _Input.TouchActorHit += AttackTarget;
    }

    private void Update() {
        if (Input.GetKey(KeyCode.D)) {
            Owner.TakeDamage(new Damage(10));
        }
        if (Input.GetKey(KeyCode.S)) {
            MoveController.Agent.isStopped = true;
            Owner.Animator.SetBool("Attack", true);
        }
    }

    private void MoveToGroundHitPoint(Vector3 point) {
        AttackController.Target = null;
        MoveController.MoveToPoint(point);
    }

    private void AttackTarget(Actor actor) {
        AttackController.Target = actor;
    }

    protected override void OnDestroy() {
        _Input.TouchGroundHit -= MoveToGroundHitPoint;
        base.OnDestroy();
    }
}