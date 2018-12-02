using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SingletonBehaviour<PlayerController> {

    public Unit Unit { get; private set; }
    public MoveController MoveController { get; private set; }
    public AttackController AttackController { get; private set; }

    private InputSystem _Input;

    protected override void Awake () {
        base.Awake();
        Unit = GetComponent<Unit>();
        MoveController = GetComponentInChildren<MoveController>();
    }
	
    protected void Start() {
        AttackController = Unit.AttackController;
        _Input = InputSystem.Instance;
        _Input.TouchGroundHit += MoveToGroundHitPoint;
        _Input.TouchActorHit += AttackTarget;
    }

    private void Update() {
        if (Input.GetKey(KeyCode.D)) {
            Unit.TakeDamage(new Damage(100));
        }
        PursueOrAttackTarget();
    }

    private void MoveToGroundHitPoint(Vector3 point) {
        AttackController.Target = null;
        MoveController.MoveToPoint(point);
    }

    private void AttackTarget(Actor actor) {
        AttackController.Target = actor;
    }

    private void PursueOrAttackTarget() {
        if(AttackController.Target != null) {
            var sqrDistToTarget = Unit.AttackController.SqrDistanceToTarget;
            if (sqrDistToTarget < Unit.AttackController.Weapon.SqrRange) {
                Unit.AttackController.Attack();
                Unit.MoveController.IsStopped = true;
            }
            else {
                var targetPos = Unit.AttackController.Target.transform.position;
                Unit.MoveController.MoveToPoint(targetPos);
            }
        }
    }

    protected override void OnDestroy() {
        _Input.TouchGroundHit -= MoveToGroundHitPoint;
        base.OnDestroy();
    }
}