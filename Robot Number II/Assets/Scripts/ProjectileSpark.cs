using UnityEngine;
using System.Collections;

public class ProjectileSpark : MonoBehaviour {
	public float speed;
	private float time = 0.0f;
	private Vector3 direction;
	private ICharacter character;
	private string characterTag;
	private IAbility attack;
	
	void Start () {
		
		//this.characterDirection = character.GetDirection();
		this.characterTag = this.character.GetTag ();
		float x = Random.Range (-1.0f, 1.0f);
		float y = Random.Range (-3.0f, -2.5f);
		direction = new Vector3(x, y, 0);
		
	}
	
	// Update is called once per frame
	void Update () {
		
		time += Time.deltaTime;
		
		
		transform.Translate (direction * speed * Time.deltaTime);//,Space.Self);
		
		if(time > 2)
		{
			Destroy(gameObject);
		}
	}
	
	void OnTriggerEnter2D (Collider2D other){
		
		if (other.tag.CompareTo("Ground") ==0) {
			Destroy (gameObject);
		}
		
		if (other.tag.CompareTo (this.characterTag) != 0 && other.tag.CompareTo ("Ground") != 0 
		    && other.tag.CompareTo ("Projectile") != 0 && other.tag.CompareTo ("Objective") != 0 
		    && other.tag.CompareTo ("DeathBox") != 0 && other.tag.CompareTo ("Rope") != 0) {
			other.SendMessage ("ApplyAbility", this.attack);
			Destroy (gameObject);
		} 
		
		
	}
	
	public void SetCharacter(ICharacter character){
		this.character = character;
	}
	
	public void SetAttack(IAbility attack) {
		this.attack = attack;
	}
	
}
