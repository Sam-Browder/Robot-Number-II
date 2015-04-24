using UnityEngine;
using System.Collections;

public class LazerAttack : IAttack {
	
	string attack = "Lazer";
	
	public string getAttack() {
		return this.attack;
	}
	
}
