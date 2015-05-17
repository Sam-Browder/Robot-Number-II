using UnityEngine;
using System.Collections;

public class ResistanceEnhancement : IItem {
	
	private string item = "ResistanceEnhancement";
	private string slot = "defense";
	private IAbility primary = new DefenseEnhancement (7f);
	private IAbility secondary = new NonAbility();
	
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
	
}
