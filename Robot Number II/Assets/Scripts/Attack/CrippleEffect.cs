using UnityEngine;
using System.Collections;

public class CrippleEffect : IEffect {

	private string effect = "cripple";
	private float duration;
	private float endTime;
	private float percentage;
	private float flatValue;

	public CrippleEffect(float duration, float flatValue, float percentage){
		this.duration = duration;
		this.flatValue = flatValue;
		this.percentage = percentage;
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

	public float GetFlatValue(){
		return this.flatValue;
	}

	public float GetPercentage(){
		return this.percentage;
	}
	
}
