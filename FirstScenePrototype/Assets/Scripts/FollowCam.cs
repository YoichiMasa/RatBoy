﻿using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {

	public Transform objectToFollow;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(objectToFollow);
	
	}
}
