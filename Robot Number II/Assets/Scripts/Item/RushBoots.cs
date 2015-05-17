using UnityEngine;
using System.Collections;

public class RushBoots : IItem {
	
	private string item = "RushBoots";
	private string slot = "utility";
	private IAbility primary = new Rush (500f, 3f);
	private IAbility secondary = new NonAbility();
	
	public RushBoots (){}
	
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
