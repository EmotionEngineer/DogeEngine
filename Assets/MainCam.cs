using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCam : MonoBehaviour
{
    public float Distance;
    public float Height;
    public float RotationDamping;
    public GameObject Target;

    private Camera _camera;

    void Start()
    {
        _camera = GameObject.Find("Camera").GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        var wantedRotationAngleYaw = Target.transform.eulerAngles.y;
        var currentRotationAngleYaw = transform.eulerAngles.y;

        var wantedRotationAnglePitch = Target.transform.eulerAngles.x;
        var currentRotationAnglePitch = transform.eulerAngles.x;

        var wantedRotationAngleRoll = Target.transform.eulerAngles.z;
        var currentRotationAngleRoll = transform.eulerAngles.z;

        currentRotationAngleYaw = Mathf.LerpAngle(currentRotationAngleYaw, wantedRotationAngleYaw, RotationDamping * Time.deltaTime);

        currentRotationAnglePitch = Mathf.LerpAngle(currentRotationAnglePitch, wantedRotationAnglePitch, RotationDamping * Time.deltaTime);

        currentRotationAngleRoll = Mathf.LerpAngle(currentRotationAngleRoll, wantedRotationAngleRoll, RotationDamping * Time.deltaTime);

        var currentRotation = Quaternion.Euler(currentRotationAnglePitch, currentRotationAngleYaw, currentRotationAngleRoll);
        transform.position = Target.transform.position;
        transform.position -= currentRotation * Vector3.forward * Distance;

        Vector3 currentUp = currentRotation * Vector3.up;
        transform.LookAt(Target.transform, currentUp);
        transform.position += currentUp * Height;
    }
}
