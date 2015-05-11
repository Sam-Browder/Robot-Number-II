using UnityEngine;
using System.Collections;

public class NonAbility : IAbility {
	
	private string ability;
	private float cd;
	private float cde;
	
	private ArrayList effects = new ArrayList(); 
	
	public NonAbility(){;
		this.cd = 0f;
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
		return Time.time;
	}
	
	public void ApplyAbility(ICharacter character){
	}
}
	
