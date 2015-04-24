using UnityEngine;
using System.Collections;

public class ProjectileAttack : IAttack {

	string attack = "Projectile";

	public string getAttack() {
		return this.attack;
	}

}
