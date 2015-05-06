using UnityEngine;
using System.Collections;

public class LazerCannon : IItem {

	private string item = "LazerCannon";
	private string slot = "weapon";
	private IAbility primary = new BasicAttack("Lazer", 10f, 10f, 10f, 10f, 0f);
	private IAbility secondary = new NonAbility ();
	private bool isDoubleHand = true;
	private bool isOffHand = false;


	public LazerCannon (){}

	public string GetItem(){
		return this.item;
	}

	public string GetSlot(){
		return this.slot;
	}

	public IAbility GetPrimaryAbility(){
		return this.primary;
	}

	public IAbility GetSecondaryAbility(){
		return this.secondary;
	}

	public bool IsDoubleHand(){
		return this.isDoubleHand;
	}

	public bool IsOffHand(){
		return this.isOffHand;
	}


}
