using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

	//private int direction;
	private Vector3 newPos;
	private Vector2 oldVeloctiy;
	private Vector2 newVelocity;
	public GameObject destPortal;
	private Component thisExit;
	private Component destExit;

	// Use this for initialization
	void Start () {


		this.thisExit = this.gameObject.GetComponentInChildren<PortalExit> ();


		this.destExit = this.destPortal.GetComponentInChildren<PortalExit> ();


		this.newPos = this.destExit.transform.position;


		//this.newPos = this.destPortal.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) {

			other.transform.position = newPos;


		}
		
		
	}
}
