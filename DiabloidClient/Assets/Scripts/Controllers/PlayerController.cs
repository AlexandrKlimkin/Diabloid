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
        AttackController = GetComponent<AttackController>();
    }
	
    protected void Start() {
        _Input = InputSystem.Instance;
        _Input.TouchGroundHit += MoveToGroundHitPoint;
    }

    private void MoveToGroundHitPoint(Vector3 point) {
        MoveController.MoveToPoint(point);
    }

    protected override void OnDestroy() {
        _Input.TouchGroundHit -= MoveToGroundHitPoint;
        base.OnDestroy();
    }
}