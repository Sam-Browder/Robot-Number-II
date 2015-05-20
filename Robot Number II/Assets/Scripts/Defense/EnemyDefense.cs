using UnityEngine;
using System.Collections;

public class EnemyDefense : MonoBehaviour,IDefenseBehavior {
	
	
	private ICharacter character;
	private float maxHealth = 30f;
	private float health;
	private float maxShieldAmount = 0f;
	private float shieldAmount;
	private float shieldRegen = 0f;
	private float shieldInterval = 1f;
	private float timeEnd;
	
	private float EMResistance = 60f;
	private float ThermalResistance = 60f;
	private float KineticResistance = 60f;
	private float ExplosiveResistance = 60f;
	
	void Start(){
		this.timeEnd = Time.time;
		this.health = this.maxHealth;
		this.shieldAmount = this.maxShieldAmount;
		this.character = gameObject.GetComponentInParent<ICharacter> ();
	}
	
	void Update(){
		shieldRegeneration ();
	}
	
	public void ApplyDamage(float EM, float Thermal, float Kinetic, float Explosive){
		float finalDamage = 0;
		
		finalDamage = DamageCalculation (EM, Thermal, Kinetic, Explosive);
		
		this.shieldAmount = this.shieldAmount - finalDamage;
		if (this.shieldAmount >= 0) {
			finalDamage = 0;
		} else {
			finalDamage = Mathf.Abs (this.shieldAmount);
			this.shieldAmount = 0;
		}
		
		this.health = this.health - finalDamage;
	}
	
	public float DamageCalculation(float EM, float Thermal, float Kinetic, float Explosive){
		float finalDamage = 0;
		float EMBonus = 0;
		float ThermalBonus = 0;
		float KineticBonus = 0;
		float ExplosiveBonus = 0;
		
		ArrayList effects = this.character.GetEffects ();
		
		for (int i = 0; i < effects.Count; i++) {
			IEffect effect = (IEffect)effects [i];
			if (effect.GetType () == typeof(ResistanceBonus)) {
				ResistanceBonus rb = (ResistanceBonus)effect;
				EMBonus += rb.GetEM ();
				ThermalBonus += rb.GetThermal ();
				KineticBonus += rb.GetKinetic ();
				ExplosiveBonus += rb.GetExplosive ();
			}
		}
		
		
		finalDamage = TypeDamageCalculation (EM, this.EMResistance + EMBonus) +
			TypeDamageCalculation (Thermal, this.ThermalResistance + ThermalBonus) +
				TypeDamageCalculation (Kinetic, this.KineticResistance + KineticBonus) +
				TypeDamageCalculation (Explosive, this.ExplosiveResistance + ExplosiveBonus);
		
		return finalDamage;
	}
	
	public float TypeDamageCalculation(float damage, float resist){
		if (resist >= 0)
			return damage * (75 / (resist + 75));
		else
			return damage * (2 - (75 / (75 - resist)));
	}
	
	public void ChargeShield(float shieldAmount){
		this.shieldAmount += shieldAmount;
	}
	
	public void RestoreHealth(float healthAmount){
		this.health += healthAmount;
	}
	
	public float GetHealth() {
		return this.health;
	}

	public void ResetHealth(float health){
		this.maxHealth = health;
		this.health = this.maxHealth;
	}

	
	public float GetShield(){
		return this.shieldAmount;
	}
	
	public void shieldRegeneration(){
		if (Time.time > this.timeEnd) {
			if (this.shieldAmount > this.maxShieldAmount - this.shieldRegen){
				this.shieldAmount = this.maxShieldAmount;
			} else {
				this.shieldAmount += this.shieldRegen;
			}
			this.timeEnd = Time.time + this.shieldInterval;
		}
	}

	public void SetResistance(double EM, double Thermal, double Kinetic, double Explosive){
		this.EMResistance = 60f * (float) EM;
		this.ThermalResistance = 60f * (float) Thermal;
		this.KineticResistance = 60f * (float) Kinetic;
		this.ExplosiveResistance = 60f * (float) Explosive;
	}
	
}

