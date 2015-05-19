using UnityEngine;
using System.Collections;

public class MeleeAnim : MonoBehaviour, IAnim {
	
	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}
	
	public void Animate(IAbility ability, ICharacter character){
		GameObject proj1 = (GameObject)Instantiate (Resources.Load ("Melee"));
		
		proj1.transform.position = new Vector3(this.transform.position.x + 0.7f, this.transform.position.y);
		proj1.SendMessage ("SetCharacter", character);
		proj1.SendMessage ("SetAttack", ability);

		GameObject proj2 = (GameObject)Instantiate (Resources.Load ("Melee"));
		
		proj2.transform.position = new Vector3(this.transform.position.x - 0.7f, this.transform.position.y);
		proj2.SendMessage ("SetCharacter", character);
		proj2.SendMessage ("SetAttack", ability);

		GameObject proj3 = (GameObject)Instantiate (Resources.Load ("Melee"));
		
		proj3.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.5f);
		proj3.SendMessage ("SetCharacter", character);
		proj3.SendMessage ("SetAttack", ability);
	}
}
