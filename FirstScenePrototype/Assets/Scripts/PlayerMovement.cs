using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	float baseSpeed = 40;
	float baseMaxSpeed = 5f;
	public float moveSpeed;
	public float jumpHeight = 400;
	public float runSpeed = 60;
	public GameObject deathParticles;
	GameObject player;
	public bool grounded;
	public bool alive = true;
	public float Velocity;

	private float maxMoveSpeed = 5f;
	public float maxRunSpeed = 10f;
	private Vector3 input;
	private Vector3 spawn;

	//new movement
	private float speed = 0.0f;
	private float direction = 0f;
	private float horizontal = 0.0f;
	private float vertical = 0.0f;
	
	public bool offLadder = true;




	//game object references
	public HealthStaminaController bars;
	[SerializeField]
	private ThirdPersonCamera gamecam;
	public FixedCameraControl gamecam2;


	// Use this for initialization
	void Start () {
		spawn = transform.position;
		player = GameObject.FindGameObjectWithTag ("Player");
		moveSpeed = baseSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		if (alive) {
						Velocity = rigidbody.velocity.magnitude;
						//raycasting for ground detection
						IsGrounded ();
						//new movement
						if (offLadder != false) {
								horizontal = Input.GetAxis ("Horizontal");
								vertical = Input.GetAxis ("Vertical");
			
								Vector3 moveDirection = StickToWorldspace (this.transform, gamecam2.currentCamera.transform, ref direction, ref speed);
			
								if (rigidbody.velocity.magnitude < maxMoveSpeed) {
										rigidbody.AddForce (moveDirection * moveSpeed);
								}
			
								//jump
								if (Input.GetKeyDown (KeyCode.Space) && grounded) {
										rigidbody.AddForce (0, jumpHeight, 0);
										bars.stamina -= bars.baseJump;
								}
			
								//jump height control
								if (Input.GetKeyUp (KeyCode.Space)) {
										if (rigidbody.velocity.y > 0) {
												rigidbody.velocity = new Vector3 (rigidbody.velocity.x, rigidbody.velocity.y / 2, rigidbody.velocity.z);
										}
					
								}
								//Run
								if (Input.GetKeyDown (KeyCode.LeftAlt)) {
										maxMoveSpeed = maxRunSpeed;
										moveSpeed = runSpeed;
										//bars.stamina -= bars.baseSprint;
										bars.baseMove = bars.baseSprint;
								}
			
								if (Input.GetKey (KeyCode.LeftAlt)) {
										//bars.stamina -= bars.baseSprint;
										bars.baseMove = bars.baseSprint;
								}
			
								if (Input.GetKeyUp (KeyCode.LeftAlt)) {
										maxMoveSpeed = baseMaxSpeed;
										moveSpeed = baseSpeed;
								}
						}
						if (bars.HP <= 0) {
								//			bars.HP = 100f;
								//			bars.stamina = 1000f;
								Instantiate (deathParticles, transform.position, Quaternion.identity);
								alive = false;
								Destroy (player);
						}
				}
	}


	void FixedUpdate (){



	}


	void OnCollisionEnter(Collision other)
	{
		if (other.transform.tag == "Enemy")
		{
			bars.HP -= 5f;
			rigidbody.AddExplosionForce(300, transform.position, 10);

			//death
			if(bars.HP < 1)
			{
			bars.HP = 100f;
			bars.stamina = 1000f;
			Instantiate (deathParticles, transform.position, Quaternion.identity);
			transform.position = spawn;
			}
		}
	
	}

	//referential shift method
	public Vector3 StickToWorldspace(Transform root, Transform camera, ref float directionOut, ref float speedOut)
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
		
		directionOut = angleRootToMove * maxMoveSpeed;

		return moveDirection;
	}

	public bool IsGrounded() {
		grounded = false;
		RaycastHit hit;
		if(Physics.Raycast(transform.position, Vector3.down, out hit, 1)) {
			//Debug.Log("Hitting: " + hit.transform.gameObject.tag);
			Debug.DrawRay(transform.position, Vector3.down);
			if(hit.transform.gameObject) {
				grounded = true;
			}
		} 
		return grounded;
	}

}