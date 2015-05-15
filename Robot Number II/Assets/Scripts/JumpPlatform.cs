﻿using UnityEngine;
using System.Collections;

public class JumpPlatform : MonoBehaviour {

	private int jumpPower = 600;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) {

			//other.attachedRigidbody.AddForce(Vector2.up * this.jumpPower);
			other.SendMessage("SetYVelocity",0.0);
			other.SendMessage("SuperJump", this.jumpPower);
		}
		

	}
}

