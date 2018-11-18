﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveController : MonoBehaviour {

    public float Speed = 5f;
    public float AngularSpeed = 1080f;
    public Vector3 Velocity { get; private set; }
    public bool IsStopped { get; set; }
    public Vector3 DestinationPoint { get; private set; }
    private bool _DestinationPointReached;
    private Vector3 _TargetPathPoint;
    private int _TargetPathPointIndex;
    private Quaternion _TargetRotation;

    public Unit Owner { get; private set; }

    public NavMeshPath Path { get; private set; }

    public bool CanMove {
        get {
            return !Owner.Dead;
        }
    }
    public bool IsMoving {
        get {
            return !IsStopped && Velocity != Vector3.zero; 
        }
    }

    private void Awake() {
        Owner = GetComponent<Unit>();
    }

    private void Start() {
        Path = new NavMeshPath();
        _TargetRotation = transform.rotation;
        IsStopped = true;
        Owner.OnDeath += OnOwnerDeath;
    }

    private void Update() {
        if (Owner.Dead)
            return;
        MoveAlongPath();
        RotateUnit();
        UpdateAnimator();
        for (int i = 0; i < Path.corners.Length - 1; i++)
            Debug.DrawLine(Path.corners[i], Path.corners[i + 1], Color.red);
    }

    public void MoveToPoint(Vector3 point) {
        IsStopped = false;
        var havePath = NavMesh.CalculatePath(transform.position, point, NavMesh.AllAreas, Path);
        if (havePath) {
            _DestinationPointReached = false;
            DestinationPoint = Path.corners[Path.corners.Length - 1];
            _TargetPathPointIndex = 1;
            _TargetPathPoint = Path.corners[_TargetPathPointIndex];
        }
    }

    private void MoveAlongPath() {
        if (_DestinationPointReached || IsStopped)
            return;
        var direction = _TargetPathPoint - transform.position;
        var sqrDistToTargetPoint = Vector3.SqrMagnitude(direction);
        if (sqrDistToTargetPoint > 0.1f) {
            Velocity = direction.normalized * Speed * Time.deltaTime;
        }
        else {
            if(_TargetPathPointIndex < Path.corners.Length - 1) {
                _TargetPathPointIndex++;
                _TargetPathPoint = Path.corners[_TargetPathPointIndex];
            }
            else {
                _DestinationPointReached = true;
                Velocity = Vector3.zero;
            }
        }
        transform.position += Velocity;
    }

    private void RotateUnit() {
        if (IsMoving)
            _TargetRotation = Quaternion.LookRotation(Velocity, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, _TargetRotation, AngularSpeed * Time.deltaTime);
    }

    public void ForceLookAt(Actor actor) {
        var view = Vector3.Scale(actor.transform.position - transform.position, new Vector3(1, 0, 1));
        _TargetRotation = Quaternion.LookRotation(view, Vector3.up);
    }

    public void UpdateAnimator() {
        Owner.Animator.SetBool("Moving", IsMoving);
    }

    private void OnOwnerDeath() {
        IsStopped = true;
    }

    private void OnDestroy() {
        Owner.OnDeath -= OnOwnerDeath;
    }
}