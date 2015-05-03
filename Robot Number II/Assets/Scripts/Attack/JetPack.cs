using UnityEngine;
using System.Collections;

public class JetPack : IAbility {

	private string ability = "JetPack";
	private float jumpPower;
	
	private ArrayList effects = new ArrayList(); 

	public JetPack(float jumpPower){
		this.jumpPower = jumpPower;
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
		rb2d.AddForce(Vector2.up * this.jumpPower);	
		
		for (int i = 0; i < effects.Count; i++) {
			IEffect effect = (IEffect) effects [i];
			effect.ApplyEffect(character);
		}
	}

}
