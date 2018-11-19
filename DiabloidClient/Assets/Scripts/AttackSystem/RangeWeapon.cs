﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : Weapon {

    public SelfDirectingProjectile ProjectilePrefab;
    public Transform ProjectileSpawnPoint;

    public override void OnStartAttack() { }

    public override void PerformAttack() {
        if (Controller.Target == null)
            return;
        var projectile = Instantiate(ProjectilePrefab, ProjectileSpawnPoint.position, ProjectileSpawnPoint.rotation);
        var dmg = new Damage(Damage, Controller.Owner, DamageType.Middle);
        var parameters = new SelfDirectingProjectileInit() { Damage = dmg, Target = Controller.Target };
        projectile.Initialize(parameters);
    }
}