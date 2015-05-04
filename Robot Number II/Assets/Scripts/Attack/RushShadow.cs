using UnityEngine;
using System.Collections;

public class RushShadow : MonoBehaviour {

	private float timeEnd;

	// Use this for initialization
	void Start () {
		this.timeEnd = Time.time + 2f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > this.timeEnd)
			Destroy (gameObject);
	}
}
