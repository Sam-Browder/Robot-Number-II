using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShieldBar : MonoBehaviour {

	private ICharacter player;
	
	// Use this for initialization
	void Start () {
		this.player = GameObject.FindGameObjectWithTag ("Player").gameObject.GetComponentInChildren<ICharacter> ();
	}
	
	// Update is called once per frame
	void Update () {
		Image image = GetComponent<Image> ();
		image.fillAmount = this.player.GetDefense ().GetShield() / 50f;
	}
}
