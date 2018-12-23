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
    private float _DefaultAngleX;
    [SerializeField]
    private float _TargetAngleY;
    [SerializeField]
    private float _MoveSmoothness;
    [SerializeField]
    private float _RotateSmothness;
    [SerializeField]
    private float _ForceRotateSmothness;

    public ICameraTarget Target { get; set; }

    public Vector3 ForwardVector {
        get {
            return Vector3.Scale(transform.forward, new Vector3(1, 0, 1)).normalized;
        }
    }


    void Start() {
        Target = PlayerController.Instance.Unit;
        InputSystem.Instance.RotateCameraLeft += RotateLeftAroundTarget;
        InputSystem.Instance.RotateCameraRight += RotateRightAroundTarget;
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
        var targetRotation = Quaternion.Euler(_DefaultAngleX, _TargetAngleY, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _RotateSmothness);
    }

    private void RotateAroundTarget(bool right)
    {
        var sideCof = right ? 1 : -1;
        _TargetAngleY += Time.deltaTime * _ForceRotateSmothness * sideCof;
    }

    private void RotateRightAroundTarget()
    {
        RotateAroundTarget(true);
    }

    private void RotateLeftAroundTarget()
    {
        RotateAroundTarget(false);
    }
}