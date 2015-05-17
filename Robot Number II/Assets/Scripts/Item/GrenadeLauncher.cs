using UnityEngine;
using System.Collections;

public class GrenadeLauncher : IItem {

	private string item = "GrenadeLauncher";
	private string slot = "weapon";
	private IAbility primary = new BasicAttack("Projectile", 0f, 5f, 10f, 10f, 0.5f);
	private IAbility secondary = new GrenadeToss(0f,10f,20f,20f,10f);

	public GrenadeLauncher (){}

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


}
