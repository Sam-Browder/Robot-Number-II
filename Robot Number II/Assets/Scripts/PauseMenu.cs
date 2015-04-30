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
			if (paused == true)
				GUI.Button (new Rect (Screen.width / 2 - 100, Screen.height / 2 + 25, 250, 50), "Options");
			if (paused == true)
				if (GUI.Button (new Rect (Screen.width / 2 - 100, Screen.height / 2 - 25, 250, 50), "Resume")) Unpause();
		}

	void Pause()
	{
		paused = true;
		Time.timeScale = 0.0f;
		Cursor.visible = true;
	}
	void Unpause()
	{
		paused = false;
		Time.timeScale = 1.0f;
		Cursor.visible = false;
	}
}
