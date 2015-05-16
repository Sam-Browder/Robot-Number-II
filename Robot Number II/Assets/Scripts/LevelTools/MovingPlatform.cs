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


	private float y1;
	private float x1;
	private float y2;
	private float x2;
	// Use this for initialization
	void Start () {

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
		
	}

	void FixedUpdate()
	{
		this.rb2d.velocity =new Vector2 ((this.x2 - this.x1)*this.direction*speedScale , (this.y2 - this.y1)*this.direction)*speedScale;
	}

	public void Reverse(){
		if (this.time > 1) {
			this.direction = this.direction * -1;
		}
	}

	public float getSpeedScale(){
		return this.speedScale;
	}
}
