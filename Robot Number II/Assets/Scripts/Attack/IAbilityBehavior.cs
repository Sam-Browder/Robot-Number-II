using UnityEngine;
using System.Collections;

public interface IAbilityBehavior{

	void ExecuteAbility(int index);
	void SetAbility(IAbility ability, int index);

}
