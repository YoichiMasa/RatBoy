using UnityEngine;
using System.Collections;

public class CameraSwitch : MonoBehaviour {

	public Camera newCamera;
	public Camera previousCamera;

	public FixedCameraControl switcher;

	void Start () {
	
	}

	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		newCamera.camera.active = true;
		previousCamera.camera.active = false;
		switcher.currentCamera = newCamera;
	}
}
