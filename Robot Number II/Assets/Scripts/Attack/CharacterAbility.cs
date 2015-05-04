using UnityEngine;
using System.Collections;

public class CharacterAbility : MonoBehaviour {

	ArrayList abilities = new ArrayList();
	//ArrayList animations = new ArrayList();
	// Use this for initialization
	void Start () {}
	// Update is called once per frame
	void Update () {}

	public void ExecuteAbility(int index){
		IAbility ability = (IAbility) this.abilities [index];
		ICharacter character = gameObject.GetComponentInParent<ICharacter> ();
		if (ability.GetType () == typeof(BasicAttack)) {
			IAnim proj = gameObject.GetComponentInChildren<ProjectileAnim> ();
			proj.Animate (ability, character);
		} else if (ability.GetType () == typeof(Rush)) {
			IAnim rush = gameObject.GetComponentInChildren<RushAnim> ();
			rush.Animate(ability,character);
			ability.ApplyAbility(character);
		} else {
			ability.ApplyAbility (character);
		}
	}

	public void SetAbility(IAbility ability, int index){
		this.abilities.Insert (index, ability);
	}
}
