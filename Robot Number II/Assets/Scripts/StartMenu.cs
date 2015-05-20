using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {

	public void startGame() {
		Application.LoadLevel(1);
	}

	public void quitGame() {
		Application.Quit ();
	}
}
