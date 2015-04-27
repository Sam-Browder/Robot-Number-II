using UnityEngine;
using System.Collections;

public class CharacterDefense : MonoBehaviour,IDefenseBehavior {
	
	//ArrayList defenses = new ArrayList();
	private float EMResistance = 60f;
	private float ThermalResistance = 60f;
	private float KineticResistance = 60f;
	private float ExplosiveResistance = 60f;

	public void DoDefense(IAttack attack, ICharacter character){
		float damage = DamageCalculation (attack);
		character.ApplyDamage (damage);
	}

	public void AddDefenseEffect(IDefense defense){}
	public void RemoveDefenseEffect(IDefense defense){}

	public float DamageCalculation(IAttack attack){
		float finalDamage = 0;
		finalDamage = finalDamage + attack.GetEMDamage() - (this.EMResistance / 10);
		finalDamage = finalDamage + attack.GetThermalDamage() - (this.ThermalResistance / 10);
		finalDamage = finalDamage + attack.GetKineticDamage() - (this.KineticResistance / 10);
		finalDamage = finalDamage + attack.GetExplosiveDamage() - (this.ExplosiveResistance / 10);
		return finalDamage;
	}


}

