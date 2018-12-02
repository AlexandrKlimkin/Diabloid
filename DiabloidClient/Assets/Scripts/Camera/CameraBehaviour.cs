using System.Collections;
using UnityEngine;

public interface ICameraTarget {
    Vector3 LookPosition { get; }
}

public class CameraBehaviour : SingletonBehaviour<CameraBehaviour> {

    [SerializeField]
    private float _DefaultHeight;
    [SerializeField]
    private float _DefaultDistance;
    [SerializeField]
    private float _DefaultAngle;
    [SerializeField]
    private float _DefaultAngleRot;
    [SerializeField]
    private float _MoveSmoothness;
    [SerializeField]
    private float _RotateSmothness;

    public ICameraTarget Target { get; set; }

    public Vector3 ForwardVector {
        get {
            return Vector3.Scale(transform.forward, new Vector3(1, 0, 1)).normalized;
        }
    }


    void Start() {
        Target = PlayerController.Instance.Unit;
    }

    void Update() {
        if (Target == null)
            return;
        ProcessMove();
        ProcessRotate();
    }

    private void ProcessMove() {
        var targetPosition = Target.LookPosition + Vector3.up * _DefaultHeight - ForwardVector * _DefaultDistance;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * _MoveSmoothness);
    }

    private void ProcessRotate() {
        var targetRotation = Quaternion.Euler(_DefaultAngle, _DefaultAngleRot, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _RotateSmothness);
    }
}
