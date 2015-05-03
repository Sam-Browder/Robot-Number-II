using UnityEngine;
using System.Collections;

public class CharacterDefense : MonoBehaviour,IDefenseBehavior {


	private ICharacter character;
	private float health = 100f;
	private float shieldAmount = 0;

	private float EMResistance = 60f;
	private float ThermalResistance = 60f;
	private float KineticResistance = 60f;
	private float ExplosiveResistance = 60f;

	ArrayList Defenses = new ArrayList();

	public void ApplyDefenseEffect(IDefense defense){
		defense.ExecuteDefense (this);
	}
	public void RemoveDefenseEffect(IDefense defense){}

	public float DamageCalculation(float EM, float Thermal, float Kinetic, float Explosive){
		float finalDamage = 0;

		finalDamage = finalDamage + EM - (this.EMResistance / 10);
		finalDamage = finalDamage + Thermal - (this.ThermalResistance / 10);
		finalDamage = finalDamage + Kinetic - (this.KineticResistance / 10);
		finalDamage = finalDamage + Explosive - (this.ExplosiveResistance / 10);

		this.shieldAmount = this.shieldAmount - finalDamage;
		if (this.shieldAmount >= 0) 
			finalDamage = 0;
		else
			finalDamage = Mathf.Abs (this.shieldAmount);

		return finalDamage;
	}

	public void ChargeShield(float shieldAmount){
		this.shieldAmount += shieldAmount;
	}

	public void ApplyDamage(float damage){
		this.health = this.health - damage;
	}

	public void ApplyEffect(IAbility ability) {
		/*ArrayList effects = ability.GetEffects ();
		for (int i = 0; i < effects.Count; i++) {
			IEffect effect = (IEffect) effects [i];
			effect.ApplyEffect();
		}*/
	}

	public float GetHealth() {
		return this.health;
	}

}

