using UnityEngine;
using System.Collections;

public class CharacterAbility : MonoBehaviour,IAbilityBehavior {

	ArrayList abilities = new ArrayList();
	// Use this for initialization
	void Start () {}
	// Update is called once per frame
	void Update () {}

	public void ExecuteAbility(int index){
		IAbility ability = (IAbility) this.abilities [index];
		if (ability.GetType () == typeof(BasicAttack)) {
			GameObject proj = (GameObject)Instantiate (Resources.Load (ability.GetAbility ()));
			
			proj.transform.position = this.transform.position;
			proj.SendMessage ("SetCharacter", gameObject.GetComponentInParent<ICharacter> ());
			proj.SendMessage ("SetAttack", ability);
		} else {
			ICharacter character = gameObject.GetComponentInParent<ICharacter> ();
			ability.ApplyAbility (character);
		}
	}

	public void SetAbility(IAbility ability, int index){
		this.abilities.Insert (index, ability);
	}
}
