using UnityEngine;
using System.Collections;

public class LadderScript : MonoBehaviour {
	public PlayerMovement player;
	//bool onLadder = false;
	public HealthStaminaController bars;
	public float jumpHeight = 400;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovement>();
	}
	
	void OnCollisionEnter(Collision col)
	{
		if(col.transform.tag == ("Player"))
		{
			player.offLadder = false;
			player.rigidbody.useGravity = false;
			float ladderX = gameObject.transform.position.x;
			float ladderY = col.gameObject.transform.position.y;
			float ladderZ = gameObject.transform.position.z;
			col.gameObject.transform.position = new Vector3(ladderX, ladderY, ladderZ);
		}
	}

	void Update()
	{
		if (player.alive == true) {
						player.IsGrounded ();
						if (player.offLadder != true) {	
								if (Input.GetKey (KeyCode.UpArrow)) {
										player.rigidbody.velocity = new Vector3 (0, 5, 0);
										bars.stamina -= bars.baseClimb;
								}
								if (Input.GetKeyDown (KeyCode.UpArrow)) {
										player.rigidbody.velocity = new Vector3 (0, 5, 0);
										bars.stamina -= bars.baseClimb;
								}
				
								if (Input.GetKeyUp (KeyCode.UpArrow)) {
										player.rigidbody.velocity = new Vector3 (0, 0, 0);
								}
				
								if (Input.GetKeyDown (KeyCode.DownArrow)) {
										player.rigidbody.velocity = new Vector3 (0, -5, 0);
										bars.stamina -= bars.baseClimb;
								}
				
								if (Input.GetKeyUp (KeyCode.DownArrow)) {
										player.rigidbody.velocity = new Vector3 (0, 0, 0);
					
								}
								if (Input.GetKey (KeyCode.DownArrow)) {
										if (player.grounded == true) {	
												player.offLadder = true;
										}
								}
								if (Input.GetKeyDown (KeyCode.Space)) {
										player.rigidbody.AddForce (50, jumpHeight, 0);
										bars.stamina -= bars.baseJump;
								}
						}
				}
	}
	
	void OnCollisionExit(Collision col){
		if(col.transform.tag == "Player")
		{
			player.offLadder = true;
			player.rigidbody.useGravity = true;
		}
		
	}
}
