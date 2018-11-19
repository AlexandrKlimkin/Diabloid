using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {
    public AttackController Controller { get; private set; }

    public float Damage;
    public float Range;
    public float ReloadTime;
    private float _CoolDownTime;

    public float SqrRange { get { return Range * Range; } }
    public virtual bool Reloaded {
        get {
            return Time.time >= _CoolDownTime;
        }
    }

    protected virtual void Awake() {
        Controller = GetComponent<AttackController>();
    }

    public virtual void Attack() {
        if (Time.time >= _CoolDownTime) {
            Controller.MoveController.IsStopped = true;
            Controller.Owner.Animator.SetTrigger("Attack");
            _CoolDownTime = Time.time + ReloadTime;
            OnStartAttack();
        }
    }

    public abstract void OnStartAttack();

    public abstract void PerformAttack();
}
