using UnityEngine;
using System.Collections;

public class SpeedBoots : IItem {
	
	private string item = "SpeedBoots";
	private string slot = "utility";
	private IAbility primary = new SpeedBurst (30f, 7f);
	private IAbility secondary = new NonAbility();
	
	public SpeedBoots (){}
	
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

