﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon {
    public override void PerformAttack() {
        if (Controller.Target == null)
            return;
        var damage = new Damage(Damage, Controller.Owner);
        Controller.Target.TakeDamage(damage);
    }
}
