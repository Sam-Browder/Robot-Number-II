using UnityEngine;
using System.Collections;

public interface IAbility{

	string GetAbility();
	ArrayList GetEffects();
	void AddEffect(IEffect effect);
	void RemoveEffect(IEffect effect);
	void ApplyAbility(ICharacter character);
	bool Cooldown();
	float GetCd();
	float GetCdEnd();

}
