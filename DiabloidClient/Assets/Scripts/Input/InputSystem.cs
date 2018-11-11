using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : SingletonBehaviour<InputSystem> {

    public const float CameraRayLength = 100f;

    public event Action<Vector3> TouchGroundHit;
    public event Action<Actor> TouchActorHit;

    public RaycastHit CameraRaycastHit {
        get {
            var cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(cameraRay, out hit, CameraRayLength);
            return hit;
        }
    }

    protected override void Awake() {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            var hit = CameraRaycastHit;
            if (hit.collider != null) {
                var hitObj = hit.collider.gameObject;

                if (Constants.Layers.Masks.Ground == (Constants.Layers.Masks.Ground | (1 << hitObj.layer))) {
                    //if (LayerMask. hitObj.layer == Constants.Layers.Masks.Ground) {
                    if (TouchGroundHit != null)
                        TouchGroundHit(hit.point);
                }
                else if (Constants.Layers.Masks.Actor == (Constants.Layers.Masks.Actor | (1 << hitObj.layer))) {
                    var actor = hit.collider.gameObject.GetComponentInChildren<Actor>();
                    if (TouchActorHit != null)
                        TouchActorHit(actor);
                }
            }

        }
    }
}