using UnityEngine;
using System.Collections;

public class NonItem : IItem {
	
	private string item = "None";
	private string slot = "None";
	private IAbility primary = new NonAbility();
	private IAbility secondary = new NonAbility ();
	private bool isDoubleHand = false;
	private bool isOffHand = false;
	
	
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
	
	public bool IsDoubleHand(){
		return this.isDoubleHand;
	}
	
	public bool IsOffHand(){
		return this.isOffHand;
	}
	
	
}
