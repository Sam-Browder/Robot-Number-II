using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {
	public GameObject blueNode;
	public GameObject orangeNode;
	public float pathSlope;
	private float direction = 1.0f;
	private Rigidbody2D rb2d;


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
		
	}

	void FixedUpdate()
	{
		this.rb2d.velocity =new Vector2 ((this.x2 - this.x1)*this.direction , (this.y2 - this.y1)*this.direction);
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("PlatformNode")) {
			this.direction = this.direction * -1;
		}
	}
}
