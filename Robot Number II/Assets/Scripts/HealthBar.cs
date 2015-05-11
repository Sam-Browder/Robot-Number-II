using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {

	private ICharacter player;

	// Use this for initialization
	void Start () {
		this.player = GameObject.FindGameObjectWithTag ("Player").gameObject.GetComponentInChildren<ICharacter> ();
	}
	
	// Update is called once per frame
	void Update () {
		Image image = GetComponent<Image> ();
		image.fillAmount = this.player.GetDefense ().GetHealth () / 100f;
	}
}
