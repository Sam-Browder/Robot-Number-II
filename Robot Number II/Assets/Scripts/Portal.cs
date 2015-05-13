using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {


	private Vector3 newPos;
	public GameObject destPortal;
	private int direction;
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
			
		
			//other.transform.position.Set(newPos.x, newPos.y, newPos.y);
			other.transform.position = newPos;
		}
		
		
	}
}
