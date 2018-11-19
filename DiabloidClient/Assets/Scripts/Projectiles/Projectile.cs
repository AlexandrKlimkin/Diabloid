using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile<T> : MonoBehaviour where T: ProjectileInit {

    public Damage Damage { get; protected set; }
    public float Speed = 20f;
    protected Vector3 _Velocity;
    protected bool _Initialized;

	protected virtual void Update () {
        if (!_Initialized) {
            //gameObject.SetActive(false);
            return;
        }
        SimulateStep(Time.deltaTime);
        if (_Velocity != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(_Velocity);
	}

    public abstract void Initialize(T parameters);

    protected abstract void SimulateStep(float time);

    protected abstract void Hit();

    protected virtual void OnDisable() {
        _Initialized = false;
        _Velocity = Vector3.zero;
        Damage = null;
    }
}

public class ProjectileInit {
    public Damage Damage;
}
