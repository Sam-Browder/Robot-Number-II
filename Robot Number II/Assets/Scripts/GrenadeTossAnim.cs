using UnityEngine;
using System.Collections;

public class GrenadeTossAnim : MonoBehaviour, IAnim {
	
	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}
	
	public void Animate(IAbility ability, ICharacter character){
		GameObject proj = (GameObject)Instantiate (Resources.Load ("Grenade"));
		
		proj.transform.position = this.transform.position;
		proj.SendMessage ("SetCharacter", character);
		proj.SendMessage ("SetAttack", ability);
	}
}

