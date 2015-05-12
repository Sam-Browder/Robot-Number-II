using UnityEngine;
using System.Collections;

public class GrenadeMovement : MonoBehaviour {
	public float speed;
	private float time = 0.0f;
	private Vector3 direction;
	private ICharacter character;
	private int characterDirection;
	private string characterTag;
	private IAbility ability;
	
	void Start () {

		this.characterDirection = character.GetDirection();
		this.characterTag = this.character.GetTag ();
		if (this.characterDirection > 0.0f) {
			direction = new Vector3 (1, 1, 0);
		} else {
			direction = new Vector3(-1, 1, 0);
		}
		
	}
	
	// Update is called once per frame
	void Update () {

		time += Time.deltaTime;

		transform.Translate (direction * speed * Time.deltaTime);//,Space.Self);
		
		if (time > 2) {
			Destroy (gameObject);
		}
			
	}


	void OnTriggerEnter2D (Collider2D other){

		if (other.tag.CompareTo("Ground") == 0){
			Explode();
			Destroy(gameObject);
		}

		
		if (other.tag.CompareTo (this.characterTag) != 0 && other.tag.CompareTo ("Ground") != 0 
		    && other.tag.CompareTo ("Projectile") != 0 && other.tag.CompareTo ("Objective") != 0 
		    && other.tag.CompareTo ("DeathBox") != 0 && other.tag.CompareTo ("Rope") != 0) {
			other.SendMessage ("ApplyAbility", this.ability);
			Explode();
			Destroy (gameObject);
		} 
		
		
	}

	public void Explode(){
		for (int i = 0; i < 10; i++) {
			GameObject proj = (GameObject)Instantiate (Resources.Load ("Spark"));
			
			proj.transform.position = this.transform.position;
			proj.SendMessage ("SetCharacter", character);
			proj.SendMessage ("SetAttack", new BasicAttack ("spark", 5f, 5f, 5f, 5f, 2f));
		}
	}
	
	public void SetCharacter(ICharacter character){
		this.character = character;
	}
	
	public void SetAttack(IAbility attack) {
		this.ability = attack;
	}
	
}
