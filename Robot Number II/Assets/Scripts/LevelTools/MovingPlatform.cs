using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {
	public GameObject blueNode;
	public GameObject orangeNode;
	private float pathSlope;
	private float direction = 1.0f;
	private Rigidbody2D rb2d;
	public float speedScale;
	private float time;
	private Component playerCheck;

	private float xVel;
	private float yVel;


	private float y1;
	private float x1;
	private float y2;
	private float x2;
	// Use this for initialization
	void Start () {

		//this.playerCheck = this.GetComponentInChildren<MovingPlatformPlayerCheck>();
		this.rb2d = gameObject.GetComponent<Rigidbody2D> ();
		this.transform.position = this.blueNode.transform.position;
		this.y1 = this.blueNode.transform.position.y;
		this.x1 = this.blueNode.transform.position.x;
		this.y2 = this.orangeNode.transform.position.y;
		this.x2 = this.orangeNode.transform.position.x;
		this.pathSlope = (y2 - y1) / (x2 - x1);
		
	}
	
	// Update is called once per frame
	void Update () {
		this.time += Time.deltaTime;
		//.Log (this.xVel);
		//this.playerCheck.SendMessage ("setVelocityX", this.xVel);
		
	}

	void FixedUpdate()
	{
		this.xVel = (this.x2 - this.x1) * this.direction * this.speedScale;
		this.yVel = (this.y2 - this.y1) * this.direction * this.speedScale;
		this.rb2d.velocity =new Vector2 (this.xVel , this.yVel);
	}

	public void Reverse(){
		if (this.time > 1) {
			this.direction = this.direction * -1;
		}
	}

	public void getSpeedScale(){
		this.playerCheck.SendMessage ("reciveSpeedScale", this.speedScale);//return this.speedScale;
	}

	public Vector2 getPlatformVelocity(){
		return this.rb2d.velocity;

	}

	public void sendVelocityX(){
		this.playerCheck.SendMessage ("setVelocityX", this.xVel*speedScale*speedScale*speedScale);
	}

	public void ApplyAbility(IAbility ability){
		//do nothing
	}
}
