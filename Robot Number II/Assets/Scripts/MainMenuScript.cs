using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {

	public TestMenu data;

	// Use this for initialization
	void Start () {
		GameObject persistentData = GameObject.FindGameObjectWithTag ("Global");
		if (persistentData != null) {
			this.data = persistentData.GetComponent<TestMenu> ();
		}
	}
	
	public void nextLevel() {
		Application.LoadLevel ("LongLevel");
	}

	public void goToLoadoutMenu() {
		//Nothing yet
	}

	public void goToShop() {
		//Nothing yet
	}
}
