using UnityEngine;
using System.Collections;

public class GcdEffect : IEffect {
	
	private string effect = "stun";
	private float duration;
	private float endTime;
	private float gcd;
	
	public GcdEffect(float gcd, float duration){
		this.duration = duration;
		this.gcd = gcd;
	}
	
	public string GetEffect() {
		return effect;
	}
	
	public void SetTime() {
		this.endTime = Time.time + this.duration;
	}

	public float GetGcd(){
		return this.gcd;
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
	
}