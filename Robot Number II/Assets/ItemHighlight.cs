using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemHighlight : MonoBehaviour {

	private string item;
	private TestMenu global;
	private Button button;

	// Use this for initialization
	void Start () {
		this.button = GetComponent<Button> ();
		this.item = this.button.name;
		this.global = GameObject.FindObjectOfType<TestMenu> ();
	}
	
	// Update is called once per frame
	void Update () {
		this.global = GameObject.FindObjectOfType<TestMenu> ();
		if (!this.global.library.GetItem (this.item).IsOwned ())
			this.button.image.color = Color.grey;
		else
			this.button.image.color = Color.white;
	}
}
