using UnityEngine;
using System.Collections;

public class JumpPack : IItem {
	
	private string item = "JumpPack";
	private string slot = "utility";
	private IAbility primary = new BurstJump(50f,5f);
	private IAbility secondary = new NonAbility();
	
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

}
