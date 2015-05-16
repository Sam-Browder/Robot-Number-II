using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

	//private int direction;
	private Vector3 newPos;
	private Vector2 oldVeloctiy;
	private Vector2 newVelocity;
	//private float angle;
	//private float Magnitude;
	private float playerAngle;
	private float degreesToRadians;
	private float radiansToDegrees;
	public GameObject destPortal;
	//public GameObject destPortalEntrance;
	//private Component thisExit;
	private Component destExit;
	private bool active = true;

	// Use this for initialization
	void Start () {

		this.degreesToRadians = Mathf.PI/180;
		this.radiansToDegrees = 57.2957795f;

		this.destExit = this.destPortal.GetComponentInChildren<PortalExit> ();
		this.newPos = this.destExit.transform.position;

	}


	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) {

			if (this.active){

				this.oldVeloctiy = other.attachedRigidbody.velocity;
				float ovx = this.oldVeloctiy.x;
				float ovy = this.oldVeloctiy.y;
			
				if(ovx == 0.0f){
					if(ovy>0.0f){
						this.playerAngle = 270f*degreesToRadians;
					}else{
						this.playerAngle = 90f*degreesToRadians;
					}
				}else{
					this.playerAngle =Mathf.Atan(ovy/ovx);
				}
				this.destPortal.SendMessage ("setActiveFalse");



				float portalOneAngle = this.gameObject.transform.rotation.eulerAngles.z* this.degreesToRadians;
				float portalTwoAngle = this.destPortal.transform.rotation.eulerAngles.z* this.degreesToRadians;



				float theta = Mathf.PI + portalTwoAngle - portalOneAngle; //this.playerAngle;//portalOneAngle;//
				float x = ovx * Mathf.Cos (theta) - ovy * Mathf.Sin(theta);
				float y = ovx * Mathf.Sin (theta) + ovy * Mathf.Cos(theta);
				this.newVelocity = new Vector2(x, y);

				other.attachedRigidbody.velocity = this.newVelocity;

				other.transform.position = newPos;

				Debug.Log (this.playerAngle);

				//Debug.Log (other.attachedRigidbody.velocity.magnitude);




			}




		}
	}

	void OnTriggerExit2D (Collider2D other){


		if(other.gameObject.CompareTag("Player")){
			this.setActiveTrue();
		}
		
	}
	public void setActiveFalse(){
		this.active = false;
	}

	public void setActiveTrue(){
		this.active = true;
	}

}












