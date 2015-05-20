using UnityEngine;
using System.Collections;
public class PauseMenu : MonoBehaviour
{

	public bool paused = false;
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape) && paused == false)
		{
			Pause ();
		}
		else if (Input.GetKeyDown (KeyCode.Escape) && paused == true)
		{
			Unpause ();
		}

	}
	
	void OnGUI ()
		{
		if (paused == true) {
			if (GUI.Button (new Rect (Screen.width / 2 - 100, Screen.height / 2 + 25, 250, 50), "Quit")) Application.Quit();
		}
		if (paused == true) {
			if (GUI.Button (new Rect (Screen.width / 2 - 100, Screen.height / 2 - 25, 250, 50), "Toggle Mute"))
				Mute();
		}
		if (paused == true) {
			if (GUI.Button (new Rect (Screen.width / 2 - 100, Screen.height / 2 - 75, 250, 50), "Resume"))
				Unpause ();
		}
		}

	void Pause()
	{
		paused = true;
		Time.timeScale = 0.0f;
		//Cursor.visible = true;
	}
	void Unpause()
	{
		paused = false;
		Time.timeScale = 1.0f;
		//Cursor.visible = false;
	}
	void Mute()
	{
		AudioSource music = GameObject.FindGameObjectWithTag ("Global").GetComponent<AudioSource> ();
		music.mute = !music.mute;
	}
}
