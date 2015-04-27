using UnityEngine;
using System.Collections;

public class CharacterAttack : MonoBehaviour,IAttackBehavior {

	IAttack primaryAttack;
	IAttack secondaryAttack;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}

	public void DoAttack(){
		GameObject proj = (GameObject) Instantiate(Resources.Load(this.primaryAttack.GetAttack()));
		
		proj.transform.position = this.transform.position;
		proj.SendMessage("SetCharacter", gameObject.GetComponentInParent<ICharacter> ());
		proj.SendMessage ("SetAttack", this.primaryAttack);
	}
	public void SwapAttack(){
		IAttack tempSwapper = this.primaryAttack;
		this.primaryAttack = this.secondaryAttack;
		this.secondaryAttack = tempSwapper;
	}
	public void SetPrimaryAttack(IAttack attack){
		this.primaryAttack = attack;
	}
	public void SetSecondaryAttack(IAttack attack){
		this.secondaryAttack = attack;
	}
}
