using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

	//private int direction;
	private Vector3 newPos;
	private Vector2 oldVeloctiy;
	private Vector2 newVelocity;
	private float angle;
	public GameObject destPortal;
	//private Component thisExit;
	private Component destExit;
	private bool active = true;

	// Use this for initialization
	void Start () {


		//this.thisExit = this.gameObject.GetComponentInChildren<PortalExit> ();


		this.destExit = this.destPortal.GetComponentInChildren<PortalExit> ();


		this.newPos = this.destExit.transform.position;

		Quaternion destRotation = this.destPortal.transform.rotation;
		Quaternion currentRotation = this.transform.rotation;

		float currentAngle = Quaternion.Angle (currentRotation, Quaternion.identity);
		float destAngle = Quaternion.Angle (destRotation, Quaternion.identity);

		//this.angle = 180.0f + destAngle-currentAngle;//Quaternion.Angle (destRotation,currentRotation); //Quaternion.identity);//


		//this.newPos = this.destPortal.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setActiveFalse(){
		this.active = false;
	}

	public void setActiveTrue(){
		this.active = true;
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) {

			if (this.active){

				this.oldVeloctiy = other.attachedRigidbody.velocity;
				Quaternion destRotation = this.destPortal.transform.rotation;

				//float playerAngle = Quaternion.Angle(other.transform.rotation, Quaternion.identity);
				float playerAngle = Quaternion.Angle(Quaternion.LookRotation(this.oldVeloctiy, Vector2.up), Quaternion.identity);
				float destAngle = Quaternion.Angle (destRotation, Quaternion.identity);

				this.destPortal.SendMessage ("setActiveFalse");

				this.angle = destAngle - playerAngle;
				//this.angle = Vector2.Angle(other.attachedRigidbody.velocity, this.destPortal.transform.


				Debug.Log (this.oldVeloctiy);
				this.newVelocity = Quaternion.Euler (0, 0, angle) * this.oldVeloctiy;
				Debug.Log (this.newVelocity);
				other.attachedRigidbody.velocity = this.newVelocity;//velocity.Set(0, 20);//this.newVelocity.x*2, this.newVelocity.y*2);



				other.transform.position = newPos;
			}




		}
	}

	void OnTriggerExit2D (Collider2D other){


		if(other.gameObject.CompareTag("Player")){
			this.setActiveTrue();
		}
		
	}
}












