using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Damage {
    public Actor Instigator;
    public float Amount;
    public DamageType Type;
}

public enum DamageType {  }