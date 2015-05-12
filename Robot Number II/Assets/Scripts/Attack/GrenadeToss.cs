using UnityEngine;
using System.Collections;

public class GrenadeToss : IAbility {
	
	private string ability = "GrenadeToss";
	private float EMDamage;
	private float ThermalDamage;
	private float KineticDamage;
	private float ExplosiveDamage;
	private float cd;
	private float cde;
	
	private ArrayList effects = new ArrayList(); 
	
	public GrenadeToss(float EMDamage, float ThermalDamage, float KineticDamage, float ExplosiveDamage, float cd){
		this.EMDamage = EMDamage;
		this.ThermalDamage = ThermalDamage;
		this.KineticDamage = KineticDamage;
		this.ExplosiveDamage = ExplosiveDamage;
		this.cd = cd;
		this.cde = Time.time;
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
	
	public bool Cooldown() {
		if (Time.time > this.cde) {
			this.cde = Time.time + this.cd;
			return true;
		} else {
			return false;
		}
	}
	
	public float GetCd(){
		return this.cd;
	}
	
	public float GetCdEnd(){
		return this.cde;
	}
	
	public void ApplyAbility(ICharacter character){
		IDefenseBehavior defense = character.GetDefense ();
		defense.ApplyDamage (this.EMDamage, this.ThermalDamage, this.KineticDamage, this.ExplosiveDamage);
		
		for (int i = 0; i < effects.Count; i++) {
			IEffect effect = (IEffect) effects [i];
			effect.ApplyEffect(character);
		}
	}
}