using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviourBS : MonoBehaviour
{
    public float DefaultHeight;
    public float Height;
    public float DefaultAngle;
    public float Sensivity;

    void Start()
    {
        transform.rotation = Quaternion.Euler(DefaultAngle, 90, 0);
    }

    void Update()
    {
        var touchDelta = GameInput.Instance.ScreenInput.CurrentTouchDelta;
        MovePosition(touchDelta);
    }

    public void MovePosition(Vector2 delta) {
        var deltaVec = new Vector3(delta.y, 0, delta.x) * Sensivity;
        transform.position += deltaVec;
    }
}
