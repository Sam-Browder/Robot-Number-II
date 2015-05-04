using UnityEngine;
using System.Collections;

public class RushAnim : MonoBehaviour, IAnim {

	private bool isRush = false;
	private Vector3 pos;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (isRush) {
			GameObject proj = (GameObject)Instantiate (Resources.Load ("Rush"));
			proj.transform.position = this.pos;
			this.isRush = false;
		}
	}

	public void Animate(IAbility ability, ICharacter character){
		GameObject chara = GameObject.Find("Player");
		this.pos = chara.transform.position;
		this.isRush = true;
	}
}
