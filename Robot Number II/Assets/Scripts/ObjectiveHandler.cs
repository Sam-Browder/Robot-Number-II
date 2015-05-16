using UnityEngine;
using System.Collections;

public class ObjectiveHandler : MonoBehaviour
{

	public string nextLevel;
	public float rotationSpeed = 1.0f;
	public int numberOfLevels;
	public int maxLevel;
	public int maxEnemiesLeft;
	// Use this for initialization
	void Start ()
	{
		this.numberOfLevels = Application.levelCount;
		this.maxLevel = this.numberOfLevels - 1;
	}
	
	// Update is called once per frame
	void Update ()
	{

		transform.Rotate (0, 0, this.rotationSpeed);


	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player") && GameObject.FindGameObjectsWithTag ("Enemy").Length <= this.maxEnemiesLeft) {
			int i = Application.loadedLevel;

			if (Application.loadedLevel == this.maxLevel) {
				Application.LoadLevel (1);
			} else {
				Application.LoadLevel (i + 1);
			}
		}
		

	}
}
