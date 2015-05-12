using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EquipmentInfo : MonoBehaviour {

	private FittingMenu fit;
	private Text txt;

	// Use this for initialization
	void Start () {
		this.fit = GameObject.FindObjectOfType<Player> ().gameObject.GetComponentInChildren<FittingMenu> ();
		this.txt = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		txt.text = this.fit.WeaponInfo ();
	}
}
