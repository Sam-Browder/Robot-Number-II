using UnityEngine;
using System.Collections;

public interface IAttack{

	string GetAttack();
	float GetEMDamage();
	float GetThermalDamage();
	float GetKineticDamage();
	float GetExplosiveDamage();

}
