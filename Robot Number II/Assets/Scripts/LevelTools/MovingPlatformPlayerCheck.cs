using UnityEngine;
using System.Collections;

public class MovingPlatformPlayerCheck : MonoBehaviour {

	private Rigidbody2D platform;
	private float platformVelocityX;
	// Use this for initialization
	void Start () {
		this.platform = this.transform.parent.gameObject.GetComponent<Rigidbody2D>();
			//this.GetComponentInParent<MovingPlatform>();
	}
	
	// Update is called once per frame
	void Update () {

		this.platformVelocityX = this.platform.velocity.x;
	}

	void OnTriggerEnter2D (Collider2D other){
		
		
		if(other.gameObject.CompareTag("Player")){
			other.SendMessage("SetPlatformVelocity", this.platformVelocityX);
			other.SendMessage("SetOnPlatform",true);
		}
		
	}

	void OnTriggerStay2D (Collider2D other){
		
		
		if(other.gameObject.CompareTag("Player")){
			other.SendMessage("SetPlatformVelocity", this.platformVelocityX);
			other.SendMessage("SetOnPlatform",true);
		}
		
	}

	void OnTriggerExit2D (Collider2D other){
		
		
		if(other.gameObject.CompareTag("Player")){
			other.SendMessage("SetOnPlatform",false);
		}
		
	}
}
