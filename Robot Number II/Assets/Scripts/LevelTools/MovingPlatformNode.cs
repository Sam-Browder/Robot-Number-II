using UnityEngine;
using System.Collections;

public class MovingPlatformNode : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("Platform")) {
			other.SendMessage("Reverse");
		}
	}
	public void ApplyAbility(IAbility ability){
		//do nothing
	}
}
