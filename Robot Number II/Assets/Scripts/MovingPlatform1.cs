using UnityEngine;
using System.Collections;

public class MovingPlatform1 : MonoBehaviour {
	public GameObject blueNode;
	public GameObject orangeNode;
	public float pathSlope;
	// Use this for initialization
	void Start () {
		this.transform.position = this.blueNode.transform.position;
		float y1 = this.blueNode.transform.position.y;
		float x1 = this.blueNode.transform.position.x;
		float y2 = this.orangeNode.transform.position.y;
		float x2 = this.orangeNode.transform.position.x;
		this.pathSlope = (y2 - y1) / (x2 - x1);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
