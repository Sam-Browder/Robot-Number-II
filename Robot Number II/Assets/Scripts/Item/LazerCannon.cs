using UnityEngine;
using System.Collections;

public class LazerCannon : IItem {

	private string item = "LazerCannon";
	private string slot = "weapon";
	private IAbility primary = new BasicAttack("Lazer", 15f, 10f, 0f, 0f, 0.2f);
	private IAbility secondary = new DeathLazer(20f,10f,0f,0f,15f);
	private bool isOwned = false;
	private int price = 30;

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

	public bool IsOwned(){
		return this.isOwned;
	}

	public int GetPrice(){
		return this.price;
	}

	public void Buy(){
		this.isOwned = true;
	}

	public string GetTooltip(){
		string tooltip = this.item + "\n";
		tooltip += "Lazer shoot, Enhanced lazer\n";
		tooltip += "PRICE: " + this.price + "\n";
		if (this.isOwned)
			tooltip += "OWNED";
		else
			tooltip += "NOT OWNED";
		
		return tooltip;
	}


}
