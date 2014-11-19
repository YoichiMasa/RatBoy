using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {

	//private variables
	PlayerMovement player;
	[SerializeField]
	private float distanceAway;
	[SerializeField]
	private float distanceUp;
	[SerializeField]
	private Transform followXform;

	//smoothing and damping
	private Vector3 velocityCamSmooth = Vector3.zero;
	[SerializeField]
	private float camSmoothDampTime = 0.1f;


	[SerializeField]
	private float smooth;
	[SerializeField]
	private Transform follow;

	private Vector3 lookDir;
	private Vector3 targetPosition;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovement> ();
		followXform = GameObject.FindWithTag("Player").transform;
		lookDir = followXform.forward;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LateUpdate(){
		if (player.alive == true) {
						Vector3 characterOffset = followXform.position + new Vector3 (0f, distanceUp, 0f);
			
						//calculate direction from camera to player, kill y, and normalaize to give a valid direction with unit magnitude
						lookDir = characterOffset - this.transform.position;
						lookDir.y = 0;
						lookDir.Normalize ();
			
			
						targetPosition = characterOffset + followXform.up * distanceUp - lookDir * distanceAway;
			
						//Wall collision
						CompensateForWalls (characterOffset, ref targetPosition);
			
						//smooth movement
						smoothPosition (this.transform.position, targetPosition);
			
						transform.LookAt (characterOffset);
				}

	}

	private void smoothPosition(Vector3 fromPos, Vector3 toPos)
	{
		this.transform.position = Vector3.SmoothDamp(fromPos, toPos, ref velocityCamSmooth, camSmoothDampTime);
	}

	private void CompensateForWalls(Vector3 fromObject, ref Vector3 toTarget)
	{
		RaycastHit wallHit = new RaycastHit();
		if(Physics.Linecast(fromObject,toTarget, out wallHit))
		{
			toTarget = new Vector3(wallHit.point.x, toTarget.y, wallHit.point.z);
		}
	}
}
