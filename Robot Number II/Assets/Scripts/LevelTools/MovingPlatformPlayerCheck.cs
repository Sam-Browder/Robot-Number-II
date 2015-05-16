using UnityEngine;
using System.Collections;

public class MovingPlatformPlayerCheck : MonoBehaviour {

	private Rigidbody2D platform;
	private float platformVelocityX;
	private float speedScale=.5f;
	// Use this for initialization
	void Start () {
		this.platform = this.transform.parent.gameObject.GetComponent<Rigidbody2D>();
		//this.speedScale = this.transform.parent.SendMessage ("getSpeedScale");
		//Component temp = this.GetComponentsInParent<MovingPlatform>;
		//this.speedScale = temp.SendMessage ("getSpeedScale");

			//this.GetComponentInParent<MovingPlatform>();
	}
	
	// Update is called once per frame
	void Update () {

		this.platformVelocityX = this.platform.velocity.x*this.speedScale;
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
