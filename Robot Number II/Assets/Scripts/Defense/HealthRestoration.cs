using UnityEngine;
using System.Collections;

public class HealthRestoration : IAbility {
	
	private string ability = "HealthRestoration";
	private float health;
	private float cd;
	private float cde;
	
	private ArrayList effects = new ArrayList(); 
	
	public HealthRestoration(float health,float cd){
		this.health = health;
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

		character.GetDefense ().RestoreHealth (this.health);

		for (int i = 0; i < effects.Count; i++) {
			IEffect effect = (IEffect) effects [i];
			effect.ApplyEffect(character);
		}
	}
	
}

