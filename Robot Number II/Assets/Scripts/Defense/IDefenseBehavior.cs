using UnityEngine;
using System.Collections;

public interface IDefenseBehavior{
	
	void DoDefense(IAttack attack);
	void ApplyDefenseEffect(IDefense defense);
	void RemoveDefenseEffect(IDefense defense);
    float DamageCalculation(IAttack attack);
	void ChargeShield(float shieldAmount);
	void ApplyDamage(float damage);
	float GetHealth();
}

