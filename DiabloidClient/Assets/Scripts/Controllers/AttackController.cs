using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour {

    public float Damage;
    public float Range;
    public float ReloadTime;
    private float _CoolDownTime;
    private float _SqrRange;
    private Actor _Target;


    public Unit Owner { get; private set; }
    public MoveController MoveController { get; private set; }
    public Weapon Weapon { get; private set; }
    public Actor Target {
        get {
            return _Target;
        }
        set {
            if (_Target != null)
                _Target.OnDeath -= OnTargetDeath;
            _Target = (value == null || value.Dead) ? null : value;
            if (_Target != null)
                _Target.OnDeath += OnTargetDeath;
        }
    }
    public float SqrDistanceToTarget { get; private set; }
    public bool TargetInRange { get { return Target != null && SqrDistanceToTarget <= _SqrRange; } }
    public bool CanAttack { get { return !Owner.Dead && Target != null; } }
    public bool IsAttacking { get; private set; }

    private void Awake() {
        Owner = GetComponentInParent<Unit>();
    }

    private void Start() {
        MoveController = Owner.MoveController;
        _SqrRange = Range * Range;
        Owner.OnDeath += OnOwnerDeath;
    }

    private void Update() {
        if (Owner.Dead)
            return;
        SqrDistanceToTarget = Target? Vector3.SqrMagnitude(transform.position - Target.transform.position) : float.PositiveInfinity;
        Attack(Target);
        UpdateAnimator();
    }

    private void Attack(Actor target) {
        if (!CanAttack) {
            IsAttacking = false;
            return;
        }
        if(TargetInRange) {
            if(Time.time >= _CoolDownTime) {
                MoveController.ForceStop();
                MoveController.ForceLookAt(Target);
                IsAttacking = true;
                _CoolDownTime = Time.time + ReloadTime;
            }
        } else {
            IsAttacking = false;
            if (MoveController != null)
                MoveController.MoveToPoint(target.transform.position);
        }         
    }

    private void UpdateAnimator() {
        Owner.Animator.SetBool("Attack", IsAttacking );
    }

    public void PerformAttack() {
        if (Target == null)
            return;
        var damage = new Damage(Damage, Owner);
        Target.TakeDamage(damage);
    }

    private void OnOwnerDeath() {
        Target = null;
        SqrDistanceToTarget = float.PositiveInfinity;
    }

    private void OnTargetDeath() {
        Target = null;
        SqrDistanceToTarget = float.PositiveInfinity;
    }

    private void OnDestroy() {
        Owner.OnDeath -= OnOwnerDeath;
    }
}
