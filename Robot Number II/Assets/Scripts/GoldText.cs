using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GoldText : MonoBehaviour {
	
	private int money;
	private Text txt;
	
	// Use this for initialization
	void Start () {
		this.money = GameObject.FindObjectOfType<TestMenu> ().money;
		this.txt = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		txt.text = ""+this.money;
	}
}
