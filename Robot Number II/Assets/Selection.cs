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
		string t = "Current Weapon: \n" + this.global.activeWeapon.GetItem() + "\n\n";
		t += "Current Defense: \n" + this.global.activeDefense.GetItem() + "\n\n";
		t += "Current Utility: \n" + this.global.activeUtility.GetItem() + "\n";
		this.txt.text = t;
	}
}
