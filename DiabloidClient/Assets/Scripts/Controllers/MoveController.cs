using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveController : MonoBehaviour {

    public Unit Owner { get; private set; }
    public NavMeshAgent Agent { get; private set; }

    public bool CanMove {
        get {
            return !Owner.Dead;
        }
    }
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
        Owner.OnDeath += OnOwnerDeath;
        //Agent.updateRotation = false;
    }

    private void Update() {
        if (Owner.Dead)
            return;
        //if(Agent.velocity != Vector3.zero)
        //    Agent.transform.rotation = Quaternion.LookRotation(Agent.velocity, Vector3.up);
        UpdateAnimator();
    }

    public void MoveToPoint(Vector3 point) {
        if (!CanMove)
            return;
        Agent.isStopped = false;
        Agent.SetDestination(point);
    }

    public void ForceStop() {
        Agent.isStopped = true;
    }

    public void ForceLookAt(Quaternion rotation) {
        Agent.transform.rotation = rotation;
    }

    public void ForceLookAt(Actor actor) {
        var view = Vector3.Scale(actor.transform.position - Agent.transform.position, new Vector3(1,0,1));
        Agent.transform.rotation = Quaternion.LookRotation(view, Vector3.up);
    }

    public void UpdateAnimator() {
        Owner.Animator.SetFloat("Speed", Agent.velocity.magnitude);
    }

    private void OnOwnerDeath() {
        ForceStop();
        Agent.enabled = false;
    }

    private void OnDestroy() {
        Owner.OnDeath -= OnOwnerDeath;
    }
}