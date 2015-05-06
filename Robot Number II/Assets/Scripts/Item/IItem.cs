using UnityEngine;
using System.Collections;

public interface IItem{

	string GetItem();
	string GetSlot();
	IAbility GetPrimaryAbility();
	IAbility GetSecondaryAbility();
	bool IsDoubleHand();
	bool IsOffHand();

}

