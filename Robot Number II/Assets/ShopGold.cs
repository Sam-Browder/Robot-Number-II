using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopGold : MonoBehaviour {
	
	private Text txt;
	private TestMenu global;
	
	// Use this for initialization
	void Start () {
		this.txt = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		this.global = GameObject.FindObjectOfType<TestMenu> ();
		txt.text = "Available Gold\n"+this.global.money;
	}
}

