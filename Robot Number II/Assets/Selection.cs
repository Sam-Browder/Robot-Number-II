using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Selection : MonoBehaviour {

	private Text txt;
	private TestMenu global;

	// Use this for initialization
	void Start () {
		this.global = GameObject.FindObjectOfType<TestMenu> ();
		this.txt = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		this.global = GameObject.FindObjectOfType<TestMenu> ();

		string weapon = "None";
		string defense = "None";
		string utility = "None";

		if (this.global.activeWeapon.IsOwned())
			weapon = this.global.activeWeapon.GetItem ();

		if (this.global.activeDefense.IsOwned())
			defense = this.global.activeDefense.GetItem ();

		if (this.global.activeUtility.IsOwned())
			utility = this.global.activeUtility.GetItem ();

		string t = "Current Weapon: \n" + weapon + "\n\n";
		t += "Current Defense: \n" + defense + "\n\n";
		t += "Current Utility: \n" + utility + "\n";
		this.txt.text = t;
	}
}
