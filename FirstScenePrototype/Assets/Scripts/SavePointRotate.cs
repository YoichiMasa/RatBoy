using UnityEngine;
using System.Collections;

public class SavePointRotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0, .5f, 0, Space.World);
	
	}
}
