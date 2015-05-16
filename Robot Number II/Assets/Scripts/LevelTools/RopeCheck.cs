using UnityEngine;
using System.Collections;

public class RopeCheck : MonoBehaviour {

	private ICharacter parent;

	void Start()
	{
		parent = gameObject.GetComponentInParent<ICharacter> ();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Rope") {
			parent.SetCanClimb (true);
			parent.SetGravity(0);
		} 
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.tag == "Rope") {
			parent.SetCanClimb (true);
			parent.SetGravity(0);
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.tag == "Rope") {
			parent.SetCanClimb (false);
			parent.SetGravity(1);
		}
	}
}
