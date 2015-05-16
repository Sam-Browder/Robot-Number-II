using UnityEngine;
using System.Collections;

public class PortalExit : MonoBehaviour {
	private Vector3 pos;
	// Use this for initialization
	void Start () {
		this.pos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Vector3 getPos(){
		return this.pos;
	}
}
