using UnityEngine;
using System.Collections;

public class CharacterControllerLogic2 : MonoBehaviour {

	[SerializeField]
	private Animator animator;
	[SerializeField]
	private float directionDampTime = .25f;
	[SerializeField]
	private ThirdPersonCamera gamecam;
	[SerializeField]
	private float directionSpeed = 3.0f;

	private float speed = 0.0f;
	private float direction = 0f;
	private float horizontal = 0.0f;
	private float vertical = 0.0f;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();

		if(animator.layerCount >= 2)
		{
			animator.SetLayerWeight(1, 1);
		}
	
	}
	
	// Update is called once per frame
	void Update () {
		if (animator)
		{
			horizontal = Input.GetAxis ("Horizontal");
			vertical = Input.GetAxis ("Vertical");

			StickToWorldspace(this.transform, gamecam.transform, ref direction, ref speed);

			animator.SetFloat ("Speed", speed);
			animator.SetFloat ("Direction", direction, directionDampTime, Time.deltaTime);

		}
	
	}

	public void StickToWorldspace(Transform root, Transform camera, ref float directionOut, ref float speedOut)
	{
		Vector3 rootDirection = root.forward;

		Vector3 stickDirection = new Vector3(horizontal, 0, vertical);

		speedOut = stickDirection.sqrMagnitude;


		Vector3 CameraDirection = camera.forward;
		CameraDirection.y = 0.0f;
		Quaternion referentialShift = Quaternion.FromToRotation(Vector3.forward, CameraDirection);


		Vector3 moveDirection = referentialShift * stickDirection;
		Vector3 axisSign = Vector3.Cross (moveDirection, rootDirection);

		float angleRootToMove = Vector3.Angle (rootDirection, moveDirection) * (axisSign.y >= 0 ? -1f : 1f);

		angleRootToMove /= 180f;

		directionOut = angleRootToMove * directionSpeed;
	}
}
