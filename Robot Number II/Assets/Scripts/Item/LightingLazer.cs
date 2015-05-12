﻿using UnityEngine;
using System.Collections;

public class LightingLazer : IItem {
	
	private string item = "LightingLazer";
	private string slot = "weapon";
	private IAbility primary = new BasicAttack("Lazer", 10f, 10f, 10f, 10f, 0f);
	private IAbility secondary = new NonAbility();
	
	
	public LightingLazer (){}
	
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