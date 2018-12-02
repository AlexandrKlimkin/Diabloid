using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
    private Actor _Owner;


    private void Awake() {
        _Owner = GetComponentInParent<Actor>();
    }

}
