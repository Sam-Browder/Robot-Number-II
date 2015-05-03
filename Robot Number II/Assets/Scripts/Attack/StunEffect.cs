using UnityEngine;
using System.Collections;

public class StunEffect : IEffect {

	private string effect = "stun";
	private float duration;
	private float endTime;
	
	public StunEffect(float duration){
		this.duration = duration;
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

}
