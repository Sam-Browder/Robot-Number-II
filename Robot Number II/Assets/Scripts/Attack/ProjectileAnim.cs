using UnityEngine;
using System.Collections;

public class ProjectileAnim : MonoBehaviour, IAnim {

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}

	public void Animate(IAbility ability, ICharacter character){
		GameObject proj = (GameObject)Instantiate (Resources.Load (ability.GetAbility ()));
		
		proj.transform.position = this.transform.position;
		proj.SendMessage ("SetCharacter", character);
		proj.SendMessage ("SetAttack", ability);
	}
}
