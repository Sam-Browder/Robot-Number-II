using UnityEngine;
using System.Collections;

public interface IDefenseBehavior{

	void ApplyDefenseEffect(IDefense defense);
	void RemoveDefenseEffect(IDefense defense);
	float DamageCalculation(float EM, float Thermal, float Kinetic, float Explosive);
	void ChargeShield(float shieldAmount);
	void ApplyDamage(float attack);
	float GetHealth();
}

