using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {

	private ICharacter player;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		this.player = GameObject.FindGameObjectWithTag ("Player").gameObject.GetComponentInChildren<ICharacter>();
		if (this.player != null) {
			Image image = GetComponent<Image> ();
			image.fillAmount = this.player.GetDefense ().GetHealth () / 100f;
		}
	}

	public void SetObj(GameObject obj){
		this.player = obj.gameObject.GetComponentInChildren<ICharacter> ();
	}
}
