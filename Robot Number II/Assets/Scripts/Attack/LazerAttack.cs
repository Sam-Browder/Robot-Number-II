using UnityEngine;
using System.Collections;

public class LazerAttack : IAttack {
	
	private string attack = "Lazer";
	private float EMDamage = 10f;
	private float ThermalDamage = 5f;
	private float KineticDamage = 0f;
	private float ExplosiveDamage = 0f;

	public string GetAttack() {
		return this.attack;
	}

	public float GetEMDamage(){
		return this.EMDamage;
	}

	public float GetThermalDamage(){
		return this.ThermalDamage;
	}

	public float GetKineticDamage(){
		return this.KineticDamage;
	}

	public float GetExplosiveDamage(){
		return this.ExplosiveDamage;
	}
	
}
