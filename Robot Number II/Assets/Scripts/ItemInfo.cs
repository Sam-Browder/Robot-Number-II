using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemInfo : MonoBehaviour {

	private Text txt;
	private TestMenu global;

	// Use this for initialization
	void Start () {
		this.global = GameObject.FindObjectOfType<TestMenu> ();
		this.txt = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void DisplayTooltip(Button button){
		if (button.name == "Health")
			this.txt.text = "PRICE: 40\nIncrease max health by 20";
		else if (button.name == "Armor")
			this.txt.text = "PRICE: 50\nIncrease all resistance by 10%";
		else
			this.txt.text = this.global.library.GetItem (button.name).GetTooltip ();
	}
}
