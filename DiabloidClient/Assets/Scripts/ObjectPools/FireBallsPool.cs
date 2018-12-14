using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallsPool : MonoBehaviourObjectPool<FireBallsPool, FireBallProjectile> {
    protected override string _PrefabPath {
        get {
            return "Prefabs/Projectiles/Fireball";
        }
    }
}