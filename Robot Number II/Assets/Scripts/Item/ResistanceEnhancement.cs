using UnityEngine;
using System.Collections;

public class ResistanceEnhancement : IItem {
	
	private string item = "ResistanceEnhancement";
	private string slot = "defense";
	private IAbility primary = new DefenseEnhancement (7f);
	private IAbility secondary = new NonAbility();
	private bool isOwned = false;
	private int price = 20;
	
	public ResistanceEnhancement (){}
	
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
		tooltip += "Increase resistance shortly\n";
		tooltip += "PRICE: " + this.price + "\n";
		if (this.isOwned)
			tooltip += "OWNED";
		else
			tooltip += "NOT OWNED";
		
		return tooltip;
	}
}
