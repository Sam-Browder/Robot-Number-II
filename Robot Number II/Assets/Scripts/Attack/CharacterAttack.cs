using UnityEngine;
using System.Collections;

public class CharacterAttack : MonoBehaviour,IAttackBehavior {

	IAttack primaryAttack;
	IAttack secondaryAttack;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}

	public void doAttack(){
		GameObject proj = (GameObject) Instantiate(Resources.Load(this.primaryAttack.getAttack()));
		
		proj.transform.position = this.transform.position;
		proj.SendMessage("SetCharacter", gameObject.GetComponentInParent<ICharacter> ());
	}
	public void swapAttack(){
		IAttack tempSwapper = this.primaryAttack;
		this.primaryAttack = this.secondaryAttack;
		this.secondaryAttack = tempSwapper;
	}
	public void setPrimaryAttack(IAttack attack){
		this.primaryAttack = attack;
	}
	public void setSecondaryAttack(IAttack attack){
		this.secondaryAttack = attack;
	}
}
