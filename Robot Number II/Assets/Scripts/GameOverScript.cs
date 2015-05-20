using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {

	public void gameOver() {
		GameObject persistentData = GameObject.FindGameObjectWithTag ("Global");
		if (persistentData != null) {
			persistentData.name = "oldPersistentData";
			persistentData.GetComponent<TestMenu>().money = persistentData.GetComponent<TestMenu>().money / 4;
		}
		Application.LoadLevel(0);
	}
}
