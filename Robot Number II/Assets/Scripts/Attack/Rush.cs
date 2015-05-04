using UnityEngine;
using System.Collections;

public class Rush : IAbility {
	
	private string ability = "JetPack";
	private float rushPower;
	private float cd;
	private float cde;
	
	private ArrayList effects = new ArrayList(); 
	
	public Rush(float rushPower, float cd){
		this.rushPower = rushPower;
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
	
	public void ApplyAbility(ICharacter character){
		Rigidbody2D rb2d = character.GetRB2D ();
		rb2d.AddForce(Vector2.right * this.rushPower * character.GetDirection());	
		
		for (int i = 0; i < effects.Count; i++) {
			IEffect effect = (IEffect) effects [i];
			effect.ApplyEffect(character);
		}
	}
	
}
