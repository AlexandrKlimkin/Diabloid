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
        Controller = GetComponentInParent<AttackController>();
    }

    public virtual bool Attack() {
        if (Reloaded) {
            _CoolDownTime = Time.time + ReloadTime;
            OnStartAttack();
            return true;
        }
        return false;
    }

    public abstract void OnStartAttack();

    public abstract void Hit();
}
