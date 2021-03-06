using UnityEngine;
using System.Collections;

public interface IDefenseBehavior{
	
	void ChargeShield(float shieldAmount);
	void RestoreHealth(float healthAmount);
	void ApplyDamage(float EM, float Thermal, float Kinetic, float Explosive);
	float TypeDamageCalculation(float damage, float resist);
	float GetHealth();
	void ResetHealth(float health);
	void shieldRegeneration();
	float GetShield();
	void SetResistance(double EM, double Thermal, double Kinetic, double Explosive);
}

