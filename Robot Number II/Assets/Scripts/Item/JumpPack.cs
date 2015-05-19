using UnityEngine;
using System.Collections;

public class JumpPack : IItem {
	
	private string item = "JumpPack";
	private string slot = "utility";
	private IAbility primary = new BurstJump(50f,5f);
	private IAbility secondary = new NonAbility();
	private bool isOwned = false;
	private int price = 40;

	public JumpPack (){}
	
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
		tooltip += "Powerful Jump\n";
		tooltip += "PRICE: " + this.price + "\n";
		if (this.isOwned)
			tooltip += "OWNED";
		else
			tooltip += "NOT OWNED";
		
		return tooltip;
	}

}
