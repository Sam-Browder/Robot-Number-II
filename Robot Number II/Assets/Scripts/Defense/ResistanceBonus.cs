using UnityEngine;
using System.Collections;

public class ResistanceBonus : IEffect {
	
	private string effect = "resistance";
	private float duration;
	private float endTime;
	private float EMResistance;
	private float ThermalResistance;
	private float KineticResistance;
	private float ExplosiveResistance;
	
	public ResistanceBonus(float EM, float Thermal, float Kinetic, float Explosive, float duration){
		this.duration = duration;
		this.EMResistance = EM;
		this.ThermalResistance = Thermal;
		this.KineticResistance = Kinetic;
		this.ExplosiveResistance = Explosive;
	}
	
	public string GetEffect() {
		return effect;
	}
	
	public void SetTime() {
		this.endTime = Time.time + this.duration;
	}
	
	public void ApplyEffect(ICharacter character){
		SetTime ();
		character.AddEffect (this);
	}
	
	public bool IsExpired() {
		if (Time.time > this.endTime)
			return true;
		else
			return false;
	}
	
	public float GetEM(){
		return this.EMResistance;
	}
	
	public float GetThermal(){
		return this.ThermalResistance;
	}

	public float GetKinetic(){
		return this.KineticResistance;
	}

	public float GetExplosive(){
		return this.ExplosiveResistance;
	}
	
}
