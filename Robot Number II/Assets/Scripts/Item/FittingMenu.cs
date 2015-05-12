using UnityEngine;
using System.Collections;

public class FittingMenu : MonoBehaviour {
	
	private IItem primaryWeapon = new NonItem();
	private IItem secondaryWeapon = new NonItem();
	private IItem defenseSystem = new NonItem();
	private IItem offenseSystem = new NonItem();
	private IItem primaryUtil = new NonItem();
	private IItem secondaryUtil = new NonItem();


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {}

	public void EquipWeapon(IItem item){
		if (item.GetSlot () == "weapon") {
			if (item.IsDoubleHand()) {
				this.primaryWeapon = item;
				this.secondaryWeapon = item;
			} else {
				if (item.IsOffHand()) {
					this.secondaryWeapon = item;
					if (this.primaryUtil.IsDoubleHand())
						this.primaryWeapon = new NonItem();
				} else {
					this.primaryWeapon = item;
					if (!this.secondaryWeapon.IsOffHand())
						this.secondaryWeapon = new NonItem();
				}
			}
		}
	}

	public string WeaponInfo(){
		string result = "";
		if (this.primaryWeapon.IsDoubleHand())
			result += "WEAPON: " + this.primaryWeapon.GetItem () + "\n";
		else 
			result += "WEAPON: " + this.primaryWeapon.GetItem () + ", " + this.secondaryWeapon.GetItem () + "\n";

		result += "DEFENSE SYSTEM: " + this.defenseSystem.GetItem () + "\n";
		result += "OFFENSE SYSTEM: " + this.offenseSystem.GetItem () + "\n";
		result += "UTILITY 1: " + this.primaryUtil.GetItem () + "\n";
		result += "UTILITY 2: " + this.secondaryUtil.GetItem () + "\n";

		return result;
	}

	public void EquipDefense(IItem item){
		this.defenseSystem = item;
	}

	public void EquipOffense(IItem item){
		this.offenseSystem = item;
	}

	public void EquipPrimaryUtil(IItem item){
		this.primaryUtil = item;
	}

	public void EquipSecondaryUtil(IItem item){
		this.secondaryUtil = item;
	}

	public void SetAbility(){
		ICharacter character = gameObject.GetComponentInParent<ICharacter> ();
		CharacterAbility abilities = character.GetAbilities();
		if (this.primaryWeapon.GetItem() != "None") {
			if (this.primaryWeapon.IsDoubleHand ()) {
				abilities.SetAbility(this.primaryWeapon.GetPrimaryAbility(),0);
				abilities.SetAbility(this.primaryWeapon.GetSecondaryAbility(),1);
			} else {
				abilities.SetAbility(this.primaryWeapon.GetPrimaryAbility(),0);
				if (this.secondaryWeapon.GetItem() != "None"){
					abilities.SetAbility(this.secondaryWeapon.GetPrimaryAbility(),1);
				}
			}
		}

		if (this.defenseSystem.GetItem() != "None")
			abilities.SetAbility (this.defenseSystem.GetPrimaryAbility (), 2);
		if (this.offenseSystem.GetItem() != "None")
			abilities.SetAbility (this.offenseSystem.GetPrimaryAbility (), 3);
		if (this.primaryUtil.GetItem() != "None")
			abilities.SetAbility (this.primaryUtil.GetPrimaryAbility (), 4);
		if (this.secondaryUtil.GetItem() != "None")
			abilities.SetAbility (this.secondaryUtil.GetPrimaryAbility (), 5);

	}
	
}
