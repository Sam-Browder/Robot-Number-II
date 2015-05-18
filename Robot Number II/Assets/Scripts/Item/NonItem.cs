using UnityEngine;
using System.Collections;

public class NonItem : IItem {
	
	private string item = "None";
	private string slot = "None";
	private IAbility primary = new NonAbility();
	private IAbility secondary = new NonAbility ();
	private bool isOwned = false;
	private int price = 0;
	
	
	public NonItem (){
	}
	
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
		return "";
	}
	
}
