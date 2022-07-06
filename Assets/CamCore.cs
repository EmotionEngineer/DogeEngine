using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CamCore : MonoBehaviour
{
    public float Distance;
	public float Height;
	public float RotationDamping;
	public GameObject Target;

    private Camera _camera;
    private string path;
 
    void Start () {
      _camera = GameObject.Find("Camera").GetComponent<Camera>();
        path = Application.dataPath;
        if (Application.platform == RuntimePlatform.OSXPlayer)
        {
            path += "/../../";
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            path += "/../";
        }
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
        Vector3 currentForw = currentRotation * Vector3.forward;
        transform.LookAt(Target.transform, currentUp);
		transform.position += currentUp * Height;
        transform.position += currentForw * 3f;
    }
 
    public void Capture(bool isSecond)
    {
    RenderTexture tempRT = new RenderTexture(800, 800, 24, RenderTextureFormat.ARGB32)
    {
      antiAliasing = 4
    };
 
    _camera.targetTexture = tempRT;
 
    RenderTexture.active = tempRT;
    _camera.Render();
 
    Texture2D image = new Texture2D(800, 800, TextureFormat.RGB24, false, true);
    image.ReadPixels(new Rect(0, 0, image.width, image.height), 0, 0);
    image.Apply();
   
    RenderTexture.active = null;
 
    byte[] bytes = image.EncodeToPNG();
    if (isSecond) File.WriteAllBytes(Path.Combine(path, "0.png"), bytes);
    else File.WriteAllBytes(Path.Combine(path, "1.png"), bytes);
    }
}
