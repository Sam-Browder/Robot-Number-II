using UnityEngine;
using System.Collections;

public class BasicAttack : IAbility {

	private string ability;
	private float EMDamage;
	private float ThermalDamage;
	private float KineticDamage;
	private float ExplosiveDamage;

	private ArrayList effects = new ArrayList(); 

	public BasicAttack(string attack, float EMDamage, float ThermalDamage, float KineticDamage, float ExplosiveDamage){
		this.ability = attack;
		this.EMDamage = EMDamage;
		this.ThermalDamage = ThermalDamage;
		this.KineticDamage = KineticDamage;
		this.ExplosiveDamage = ExplosiveDamage;
	}

	public string GetAbility() {
		return this.ability;
	}

	public ArrayList GetEffects(){
		return this.effects;
	}

	public void AddEffect(IEffect effect){
		this.effects.Add (effect);
	}

	public void RemoveEffect(IEffect effect){
		this.effects.Remove (effect);
	}

	public void ApplyAbility(ICharacter character){
		IDefenseBehavior defense = character.GetDefense ();
		float damage = defense.DamageCalculation (this.EMDamage, this.ThermalDamage, this.KineticDamage, this.ExplosiveDamage);
		defense.ApplyDamage (damage);

		for (int i = 0; i < effects.Count; i++) {
			IEffect effect = (IEffect) effects [i];
			effect.ApplyEffect(character);
		}
	}

	
}
