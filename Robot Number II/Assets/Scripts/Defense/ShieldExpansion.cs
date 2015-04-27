using UnityEngine;
using System.Collections;

public class ShieldExpansion: IDefense{

	private string defense = "ShieldExpansion";
	private float shieldAmount;

	public ShieldExpansion(int shieldAmount){
		this.shieldAmount = shieldAmount;
	}
	
	public string GetDefense(){
		return this.defense;
	}

	public void ExecuteDefense(IDefenseBehavior defense){
		defense.ChargeShield (this.shieldAmount);
	}

	
}
