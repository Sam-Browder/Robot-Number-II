using UnityEngine;
using System.Collections;

public class BurstJumpAnim : MonoBehaviour, IAnim {
	
	private bool isRush = false;
	private Vector3 pos;
	private int numOfSpark = 100;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void Animate(IAbility ability, ICharacter character){

		for (int i = 0; i < this.numOfSpark; i++) {

			GameObject proj = (GameObject)Instantiate (Resources.Load ("Spark"));
		
			proj.transform.position = this.transform.position;
			proj.SendMessage ("SetCharacter", character);
			proj.SendMessage ("SetAttack", ability);
		}
	}	
}
