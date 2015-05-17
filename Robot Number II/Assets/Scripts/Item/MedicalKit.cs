using UnityEngine;
using System.Collections;

public class MedicalKit : IItem {
	
	private string item = "MedicalKit";
	private string slot = "defense";
	private IAbility primary = new HealthRestoration (20f, 20f);
	private IAbility secondary = new NonAbility();
	
	public MedicalKit (){}
	
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

