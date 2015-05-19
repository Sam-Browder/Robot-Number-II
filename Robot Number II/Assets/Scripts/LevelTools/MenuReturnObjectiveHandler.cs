using UnityEngine;
using System.Collections;

public class MenuReturnObjectiveHandler : MonoBehaviour {

	public int nextLevel;
	public float rotationSpeed = 1.0f;
	public int numberOfLevels;
	public int maxLevel;

	// Use this for initialization
	void Start () {
		this.numberOfLevels = Application.levelCount;
		this.maxLevel = this.numberOfLevels - 1;
		int i = Application.loadedLevel;
		if(Application.loadedLevel == this.maxLevel){
				this.nextLevel = 1;
			}else{
				this.nextLevel = i+1;
			}
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.Rotate(0,0,this.rotationSpeed);
		
		
	}
	
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) {
			GameObject persistentData = GameObject.FindGameObjectWithTag("Global");
			persistentData.name = "oldPersistentData";
			TestMenu menuData = persistentData.GetComponent<TestMenu>();
			menuData.nextLevel = this.nextLevel;
			Application.LoadLevel(0);
		}
		
		
	}
}
