using UnityEngine;
using System.Collections;

public interface ICharacter{
	
	void Die();
	void SetGrounded(bool grounded);
	void SetCanClimb(bool canClimb);
	void SetGravity(float grav);
	void ApplyDefense(IAttack attack);
	int GetDirection();
	string GetTag();
	Vector3 GetPosition();
	bool GetCanClimb();
}

abstract public class Nondamageable{




}