using UnityEngine;
using System.Collections;

public class DeathLazerAnim : MonoBehaviour, IAnim {

	private float t;
	private bool shoot = false;
	private int numOfLazer = 10;
	private int count;
	private IAbility ability;
	private ICharacter character;
	private float interval;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		if (this.shoot) {
			if (Time.time > this.t) {

				GameObject proj = (GameObject)Instantiate (Resources.Load ("DeathLazer"));
				
				proj.transform.position = this.transform.position;
				proj.SendMessage ("SetCharacter", character);
				proj.SendMessage ("SetAttack", ability);

				this.count ++;
				t = Time.time + this.interval;
				if (this.count >= this.numOfLazer)
					this.shoot = false;
			}
		}
	}
	
	public void Animate(IAbility ability, ICharacter character){
		this.ability = ability;
		this.character = character;

		GameObject proj = (GameObject)Instantiate (Resources.Load ("DeathLazer"));
		
		proj.transform.position = this.transform.position;
		proj.SendMessage ("SetCharacter", character);
		proj.SendMessage ("SetAttack", ability);

		this.interval = 3 / this.numOfLazer;
		this.shoot = true;
		t = Time.time + this.interval;
		this.count = 0;
	}
}
