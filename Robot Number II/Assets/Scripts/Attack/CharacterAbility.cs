using UnityEngine;
using System.Collections;

public class CharacterAbility : MonoBehaviour {

	public ArrayList abilities = new ArrayList();
	//ArrayList animations = new ArrayList();
	// Use this for initialization
	void Start () {
		for (int i = 0; i < 4; i++) {
			this.abilities.Add (new NonAbility ());
		}
	}
	// Update is called once per frame
	void Update () {}

	public void ExecuteAbility(int index){
		IAbility ability = (IAbility) this.abilities [index];
		ICharacter character = gameObject.GetComponentInParent<ICharacter> ();
		if (ability.GetType () == typeof(BasicAttack) && ability.Cooldown ()) {
			IAnim proj = gameObject.GetComponentInChildren<ProjectileAnim> ();
			proj.Animate (ability, character);
		} else if (ability.GetType () == typeof(Rush) && ability.Cooldown ()) {
			IAnim rush = gameObject.GetComponentInChildren<RushAnim> ();
			rush.Animate (ability, character);
			ability.ApplyAbility (character);
		} else if (ability.GetType () == typeof(BurstJump) && ability.Cooldown ()) {
			IAnim jump = gameObject.GetComponentInChildren<BurstJumpAnim> ();
			jump.Animate (new BasicAttack ("Projectile", 2.5f, 2.5f, 2.5f, 2.5f, 0.5f), character);
			ability.ApplyAbility (character);
		} else if (ability.GetType () == typeof(GrenadeToss) && ability.Cooldown ()) {
			IAnim toss = gameObject.GetComponentInChildren<GrenadeTossAnim> ();
			toss.Animate (ability, character);
		} else if (ability.GetType () == typeof(DeathLazer) && ability.Cooldown ()) {
			IAnim lazer = gameObject.GetComponentInChildren<DeathLazerAnim> ();
			lazer.Animate (ability, character);
		} else if (ability.GetType () == typeof(MeleeAttack) && ability.Cooldown ()) {
			IAnim melee = gameObject.GetComponentInChildren<MeleeAnim> ();
			melee.Animate (ability, character);
		} else {
			if (ability != null){
				if (ability.Cooldown())
					ability.ApplyAbility (character);
			}
		}
	}

	public void SetAbility(IAbility ability, int index){
		if (index < this.abilities.Count)
			this.abilities [index] = ability;
		else
			this.abilities.Insert (index, ability);
	}
}
