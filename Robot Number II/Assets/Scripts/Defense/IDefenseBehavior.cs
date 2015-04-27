using UnityEngine;
using System.Collections;

public interface IDefenseBehavior{
	
	void DoDefense(IAttack attack, ICharacter character);
	void AddDefenseEffect(IDefense defense);
	void RemoveDefenseEffect(IDefense defense);
    float DamageCalculation(IAttack attack);
	
}

