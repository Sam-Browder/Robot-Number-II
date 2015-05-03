using UnityEngine;
using System.Collections;

public class Rush : IAbility {
	
	private string ability = "JetPack";
	private float rushPower;
	
	private ArrayList effects = new ArrayList(); 
	
	public Rush(float rushPower){
		this.rushPower = rushPower;
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
	
	public void ApplyAbility(ICharacter character){
		Rigidbody2D rb2d = character.GetRB2D ();
		rb2d.AddForce(Vector2.right * this.rushPower * character.GetDirection());	
		
		for (int i = 0; i < effects.Count; i++) {
			IEffect effect = (IEffect) effects [i];
			effect.ApplyEffect(character);
		}
	}
	
}
