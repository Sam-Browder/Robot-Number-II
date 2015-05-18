using UnityEngine;
using System.Collections;

public interface IItem{

	string GetItem();
	string GetSlot();
	IAbility GetPrimaryAbility();
	IAbility GetSecondaryAbility();
	bool IsOwned();
	int GetPrice();
	void Buy();
	string GetTooltip();


}

