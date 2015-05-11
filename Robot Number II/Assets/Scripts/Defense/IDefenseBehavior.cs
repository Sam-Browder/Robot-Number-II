using UnityEngine;
using System.Collections;

public interface IDefenseBehavior{
	
	void ChargeShield(float shieldAmount);
	void RestoreHealth(float healthAmount);
	void ApplyDamage(float EM, float Thermal, float Kinetic, float Explosive);
	float TypeDamageCalculation(float damage, float resist);
	float GetHealth();
	void shieldRegeneration();
	float GetShield();
}

