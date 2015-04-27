using UnityEngine;
using System.Collections;

public class CharacterDefense : MonoBehaviour,IDefenseBehavior {


	private ICharacter character;
	private float health = 100f;
	private float shieldAmount = 0;
	ArrayList Defenses = new ArrayList();
	private float EMResistance = 60f;
	private float ThermalResistance = 60f;
	private float KineticResistance = 60f;
	private float ExplosiveResistance = 60f;

	public void DoDefense(IAttack attack){
		float damage = DamageCalculation (attack);
		ApplyDamage (damage);
	}

	public void ApplyDefenseEffect(IDefense defense){
		defense.ExecuteDefense (this);
	}
	public void RemoveDefenseEffect(IDefense defense){}

	public float DamageCalculation(IAttack attack){
		float finalDamage = 0;

		finalDamage = finalDamage + attack.GetEMDamage() - (this.EMResistance / 10);
		finalDamage = finalDamage + attack.GetThermalDamage() - (this.ThermalResistance / 10);
		finalDamage = finalDamage + attack.GetKineticDamage() - (this.KineticResistance / 10);
		finalDamage = finalDamage + attack.GetExplosiveDamage() - (this.ExplosiveResistance / 10);

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

	public float GetHealth() {
		return this.health;
	}

}

