using UnityEngine;
using System.Collections;

public class FittingMenu : MonoBehaviour {
	
	private IItem weapon = new NonItem();
	private IItem defense = new NonItem();
	private IItem utility = new NonItem();

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {}

	public void Equip(IItem item){
		switch (item.GetSlot ()) {
		case "weapon":
			this.weapon = item;
			break;
		case "defense":
			this.defense = item;
			break;
		case "utility":
			this.utility = item;
			break;
		default:
			break;
		}
	}

	public string WeaponInfo(){
		string result = "";
		result += "WEAPON: " + this.weapon.GetItem () + "\n\n";
		result += "DEFENSE: " + this.defense.GetItem () + "\n\n";
		result += "UTILITY : " + this.utility.GetItem () + "\n";

		return result;
	}

	public void SetAbility(){
		ICharacter character = gameObject.GetComponentInParent<ICharacter> ();
		CharacterAbility abilities = character.GetAbilities();
		abilities.SetAbility(this.weapon.GetPrimaryAbility(),0);
		abilities.SetAbility(this.weapon.GetSecondaryAbility(),1);
		abilities.SetAbility (this.defense.GetPrimaryAbility (), 2);
		abilities.SetAbility (this.utility.GetPrimaryAbility (), 3);

	}
	
}
