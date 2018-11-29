using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour {

    //public float Damage;
    public float Range;
    public float HitTime;
    public float AttackTime;
    public float SqrRange { get { return Range * Range; } }
    //public float ReloadTime;
    //private float _CoolDownTime;
    //private float _SqrRange;
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
            SqrDistanceToTarget = Target != null ? Vector3.SqrMagnitude(transform.position - Target.transform.position) : float.PositiveInfinity;
        }
    }
    public float SqrDistanceToTarget { get; private set; }
    public bool TargetInRange { get { return Target != null && SqrDistanceToTarget <= Weapon.SqrRange; } }
    public bool CanAttack { get { return !Owner.Dead && Target != null; } }
    public bool Attacking { get; set; }

    private void Awake() {
        Owner = GetComponent<Unit>();
        Weapon = GetComponentInChildren<Weapon>();
    }

    private void Start() {
        MoveController = Owner.MoveController;
        Owner.OnDeath += OnOwnerDeath;
        Attacking = false;
    }

    private void Update() {
        if (Owner.Dead)
            return;
        SqrDistanceToTarget = Target != null ? Vector3.SqrMagnitude(transform.position - Target.transform.position) : float.PositiveInfinity;
        if (Attacking) {
            MoveController.ForceLookAt(Target);
        }
    }

    public void PerformAttack() {
        if (!CanAttack || Attacking)
            return;
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine() {
        if (Weapon.Attack()) {
            Attacking = true;
            StartCoroutine(HitRoutine());
            Owner.Animator.SetTrigger("Attack");
            yield return new WaitForSeconds(AttackTime);
            Attacking = false;
        }
    }

    private IEnumerator HitRoutine() {
        yield return new WaitForSeconds(HitTime);
        PerformHit();
    }

    private void PerformHit() {
        Weapon.Hit();
    }

    public bool CheckActorInVisionRange(Actor other, out float sqrDist) {
        sqrDist = SqrDistanceToActor(other);
        if (sqrDist < Range * Range)
            return true;
        else
            return false;
    }

    public bool CheckActorInWeaponRange(Actor other, out float sqrDist) {
        sqrDist = SqrDistanceToActor(other);
        if (Weapon == null)
            return false;
        if (sqrDist < Weapon.SqrRange)
            return true;
        else
            return false;
    }

    public float SqrDistanceToActor(Actor other) {
        if (other == null)
            return float.PositiveInfinity;
        return Vector3.SqrMagnitude(Owner.transform.position - other.transform.position);
    }

    private void OnOwnerDeath() {
        Target = null;
        //SqrDistanceToTarget = float.PositiveInfinity;
    }

    private void OnTargetDeath() {
        Target = null;
        //SqrDistanceToTarget = float.PositiveInfinity;
    }

    private void OnDestroy() {
        Owner.OnDeath -= OnOwnerDeath;
    }
}
