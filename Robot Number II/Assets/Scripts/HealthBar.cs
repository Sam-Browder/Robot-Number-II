using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {

	private CharacterDefense defense;
	
	// Use this for initialization
	void Start () {
		this.defense = GameObject.FindObjectOfType<Player> ().gameObject.GetComponentInChildren<CharacterDefense> ();
	}
	
	// Update is called once per frame
	void Update () {
		Image image = GetComponent<Image> ();
		image.fillAmount = this.defense.GetHealth () / 100f;
	}
}
