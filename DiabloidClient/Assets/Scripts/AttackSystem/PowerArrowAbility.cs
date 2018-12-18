using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerArrowAbility : Ability
{
    public Transform ProjectileSpawnPoint;
    public float Damage;
    public float ExplosionRadius;
    public float MaxDistance;

    public Vector3 Direction; // КОСТЫЛЬ

    protected override void Awake()
    {
        base.Awake();
    }

    public override void UseAbility()
    {
        var projectile = FireBallsPool.Instance.GetObject();
        projectile.transform.position = ProjectileSpawnPoint.position;
        projectile.transform.rotation = ProjectileSpawnPoint.rotation;
        var dmg = new Damage(Damage, Owner, DamageType.Middle);
        var parameters = new DirectedProjectileInit() { Damage = dmg, ExplosionRadius = this.ExplosionRadius,
            MaxDistance = this.MaxDistance, SpawnPoint = ProjectileSpawnPoint.position, Direction = this.Direction };
        projectile.Initialize(parameters);
    }
}