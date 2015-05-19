using UnityEngine;
using System.Collections;

public class LightingLazer : IItem {
	
	private string item = "LightingLazer";
	private string slot = "weapon";
	private IAbility primary = new BasicAttack("Lazer", 10f, 10f, 10f, 10f, 0f);
	private IAbility secondary = new NonAbility();
	private bool isOwned = false;
	private int price = 1;
	
	
	public LightingLazer (){}
	
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
		tooltip += "PRICE: " + this.price + "\n";
		if (this.isOwned)
			tooltip += "OWNED";
		else
			tooltip += "NOT OWNED";
		
		return tooltip;
	}

	
}