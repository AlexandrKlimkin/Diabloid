using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage {
    public Actor Instigator;
    public float Amount;

    public Damage(float amount, Actor instigator = null) {
        Amount = amount;
        Instigator = instigator;
    }
}

public enum DamageType {  }