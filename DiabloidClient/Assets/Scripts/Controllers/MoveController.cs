using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveController : MonoBehaviour {

    public Unit Owner { get; private set; }
    public NavMeshAgent Agent { get; private set; }

    public bool CanMove { get; private set; }
    public bool IsMoving {
        get {
            return !Agent.isStopped && Agent.velocity != Vector3.zero; 
        }
    }

    private void Awake() {
        Owner = GetComponent<Unit>();
        Agent = GetComponent<NavMeshAgent>();
    }

    private void Start() {
        CanMove = true;
    }

    public void MoveToPoint(Vector3 point) {
        if(CanMove)
            Agent.SetDestination(point);
    }
}