#pragma strict

var grounded : boolean;

function Start () {

}

function Update () {
	//isGrounded();
	
    // jump here
    if(grounded) {
    	//Jump();
    }
}

function IsGrounded() {
	grounded = false;
	var hit : RaycastHit;
	if(Physics.Raycast(transform.position, Vector3.down, hit, 1)) {
		Debug.Log("Hitting: " + hit.transform.gameObject.tag);
		
		if(hit.transform.gameObject.tag == "Ground") {
			grounded = true;
		}
	} 
}