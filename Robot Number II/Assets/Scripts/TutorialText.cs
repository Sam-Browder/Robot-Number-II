using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialText : MonoBehaviour {

	private Text txt;
	private string tutorial;
	public StartLocation startTimerLocation;
	public EndLocation skipTextLocation;
	public float time;
	private float textDisplayTime = 500;
	
	// Use this for initialization
	void Start () {
		this.txt = this.GetComponent<Text> ();
		this.tutorial = this.txt.text;
		this.txt.text = "";
		this.startTimerLocation = (StartLocation) this.GetComponentInChildren<StartLocation> ();
		this.skipTextLocation = (EndLocation) this.GetComponentInChildren<EndLocation> ();

	}
	
	// Update is called once per frame
	void Update () {

		if (startTimerLocation.isTouched () && !skipTextLocation.isTouched ()) {

			if (time == 0) {

				if (textDisplayTime == 0) {
					this.txt.text = "";
				} else {
					this.textDisplayTime--;
					this.txt.text = tutorial;
				}

			} else {
				this.txt.text = "";
				time--;

			}

		} else if(skipTextLocation.isTouched()){
			if(textDisplayTime == 0) {
				this.txt.text = "";
			} else {
				this.textDisplayTime--;
			}
		}
	}
}
