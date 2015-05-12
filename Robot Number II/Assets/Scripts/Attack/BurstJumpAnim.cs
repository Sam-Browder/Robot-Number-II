using UnityEngine;
using System.Collections;

public class BurstJumpAnim : MonoBehaviour, IAnim {
	
	private bool isBurst = false;
	private int numOfSpark = 100;
	private int count = 0;
	private IAbility ability;
	private ICharacter character;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (this.isBurst) {
			if (this.count >= 100)
				this.isBurst = false;

			GameObject proj = (GameObject)Instantiate (Resources.Load ("Spark"));
			
			proj.transform.position = this.transform.position;
			proj.SendMessage ("SetCharacter", character);
			proj.SendMessage ("SetAttack", ability);

			this.count ++;
		}

	}
	
	public void Animate(IAbility ability, ICharacter character){

		GameObject proj = (GameObject)Instantiate (Resources.Load ("Spark"));
		
		proj.transform.position = this.transform.position;
		proj.SendMessage ("SetCharacter", character);
		proj.SendMessage ("SetAttack", ability);

		this.ability = ability;
		this.character = character;
		this.isBurst = true;
		this.count = 0;
	}	
}
