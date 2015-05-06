using UnityEngine;
using System.Collections;

public interface ICharacter{
	
	void Die();
	void SetGrounded(bool grounded);
	void SetCanClimb(bool canClimb);
	void SetGravity(float grav);
	int GetDirection();
	string GetTag();
	Vector3 GetPosition();
	bool GetCanClimb();
	Rigidbody2D GetRB2D();


	void ApplyAbility(IAbility ability);
	void AddEffect(IEffect effect);
	ArrayList GetEffects();
	void CheckStatus();
	void EffectExpiration();
	IDefenseBehavior GetDefense();
	CharacterAbility GetAbilities();
	FittingMenu GetFitting();
}

abstract public class Nondamageable{
	
	public void ApplyAbility(IAbility ability){}

}