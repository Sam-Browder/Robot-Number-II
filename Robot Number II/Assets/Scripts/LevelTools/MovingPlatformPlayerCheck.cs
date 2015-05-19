using UnityEngine;
using System.Collections;

public class MovingPlatformPlayerCheck : MonoBehaviour {

	private Rigidbody2D platform;
	private Component platformScript;
	private float platformVelocityX;
	private float speedScale;
	// Use this for initialization
	void Start () {

		this.platform = this.transform.parent.gameObject.GetComponent<Rigidbody2D>();
		this.platformScript = this.GetComponentInParent<MovingPlatform> ();
		//this.platform = this.transform.parent.GetComponents<Rigidbody2D> ();
		//this.speedScale = this.transform.parent.SendMessage ("getSpeedScale");
		//Component temp = this.GetComponentsInParent<MovingPlatform>;
		//this.speedScale = temp.SendMessage ("getSpeedScale");

			//this.GetComponentInParent<MovingPlatform>();
	}
	
	// Update is called once per frame
	void Update () {
		//this.platform.SendMessage ("getSpeedScale");
		//this.platform.SendMessage ("sendVelocityX");
		//this.platformVelocityX = this.platform.velocity.x*this.speedScale;
	}

	void reciveSpeedScale(float sc){
		this.speedScale = sc;

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
	public void setVelocityX(float Xvel){
		this.platformVelocityX = Xvel;
		//Debug.Log (this.platformVelocityX);
	}
	void Reverse(){
	// do nothing
	}
	void ApplyAbility(){
		//do nothing
	}
}
