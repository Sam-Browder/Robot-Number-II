using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {


	private Vector3 newPos;
	public GameObject destPortal;
	private int direction;
	private Vector2 oldVeloctiy;
	private Vector2 newVelocity;
	// Use this for initialization
	void Start () {

		if(this.destPortal.transform.localScale.x > 0){
			this.direction = 1;
		}else {
			this.direction = -1;
		}
	
		this.newPos = new Vector3 (destPortal.transform.position.x + (this.direction* 1f),
		                      destPortal.transform.position.y,
		                      destPortal.transform.position.z);


		//this.newPos = this.destPortal.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) {
			//this.oldVeloctiy = other.attachedRigidbody.velocity;

			//Vector3 temp = new Vector3(this.oldVeloctiy.x, this.oldVeloctiy.y, 0);


			//Quaternion quat = Quaternion.AngleAxis(this.destPortal.transform.rotation.z, Vector3.forward);



			//temp = temp*quat;

			//this.newVelocity = new Vector2(temp.x,temp.y);
			 
		
			//other.transform.position.Set(newPos.x, newPos.y, newPos.y);
			other.transform.position = newPos;
			//other.attachedRigidbody.velocity.Set(this.newVelocity.x, this.newVelocity.y);

		}
		
		
	}
}
