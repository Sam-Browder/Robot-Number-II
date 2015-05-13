using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {


	private Vector3 newPos;
	public GameObject destPortal;
	// Use this for initialization
	void Start () {
		this.newPos = new Vector3 (destPortal.transform.position.x, //+ ((float)this.transform.localScale.x * 3f),
		                      destPortal.transform.position.y,
		                      destPortal.transform.position.y);
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
