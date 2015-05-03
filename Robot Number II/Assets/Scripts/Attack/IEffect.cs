using UnityEngine;
using System.Collections;

public interface IEffect{
	
	string GetEffect();
	void SetTime();
	void ApplyEffect(ICharacter character);
	bool IsExpired();
	
}

