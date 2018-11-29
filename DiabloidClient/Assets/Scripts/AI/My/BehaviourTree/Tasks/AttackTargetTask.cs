using System.Collections;
using System.Collections.Generic;
using Tools.BehaviourTree;
using UnityEngine;

public class AttackTargetTask : UnitTask {

    public override TaskStatus Run() {
        if ((Unit.AttackController.Target == null || Unit.AttackController.Target.Dead) && !Unit.AttackController.Attacking) {
            //Unit.AttackController.Attacking = false;
            return TaskStatus.Success;
        }
        var sqrDistToTarget = Unit.AttackController.SqrDistanceToTarget;
        if (sqrDistToTarget > Unit.AttackController.Weapon.SqrRange && !Unit.AttackController.Attacking) {
            //Unit.AttackController.Attacking = false;
            return TaskStatus.Failure;
        }
        if(Unit.AttackController.Weapon.Reloaded)
            Unit.AttackController.PerformAttack();
        return TaskStatus.Running;
    }
}
