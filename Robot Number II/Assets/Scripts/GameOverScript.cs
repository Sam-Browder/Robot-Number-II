using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {

	public void gameOver() {
		GameObject persistentData = GameObject.FindGameObjectWithTag ("Global");
		if (persistentData != null) {
			persistentData.name = "oldPersistentData";
		}
		Application.LoadLevel("Menu");
	}
}
