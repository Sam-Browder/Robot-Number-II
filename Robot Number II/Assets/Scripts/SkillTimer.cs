using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillTimer : MonoBehaviour {

	private CharacterAbility ability;
	private int index = 0;
	
	// Use this for initialization
	void Start () {
		this.ability = GameObject.FindGameObjectWithTag ("Player").gameObject.GetComponentInChildren<CharacterAbility> ();
	}
	
	// Update is called once per frame
	void Update () {
		Image image = GetComponent<Image> ();
		if (this.ability != null && this.ability.abilities [index] != null) {
			print ("lol");
			IAbility ability = (IAbility)this.ability.abilities [index];
			image.fillAmount = (ability.GetCdEnd () - Time.time) / ability.GetCd ();
		}
	}

	public void SetIndex(int index){
		this.index = index;
	}
}
