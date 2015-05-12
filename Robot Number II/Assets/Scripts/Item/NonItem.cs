using UnityEngine;
using System.Collections;

public class NonItem : IItem {
	
	private string item = "None";
	private string slot;
	private IAbility primary;
	private IAbility secondary;
	private bool isDoubleHand;
	private bool isOffHand;
	
	
	public NonItem (){}
	
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
