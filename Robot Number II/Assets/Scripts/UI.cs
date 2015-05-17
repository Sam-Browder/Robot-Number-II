using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour {

	private GameObject itemInfo;
	private bool display = false;

	// Use this for initialization
	void Start () {
//		GameObject player = GameObject.FindGameObjectWithTag ("Player");

		this.itemInfo = GameObject.Find ("ItemMenu");
		this.itemInfo.SetActive (display);

		SkillTimer[] timers = gameObject.GetComponentsInChildren<SkillTimer>();
		timers [0].SetIndex (0);
		timers [1].SetIndex (1);
		timers [2].SetIndex (2);
		timers [3].SetIndex (3);

		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		for (int i = 0; i < enemies.Length; i++) {
			GameObject newEnemyHealthBar = (GameObject)Instantiate (Resources.Load ("EnemyHealthBar"));
			newEnemyHealthBar.transform.SetParent (this.transform);
			EnemyHealthBG eHBG = newEnemyHealthBar.gameObject.GetComponentInChildren<EnemyHealthBG> ();
			eHBG.SetObj (enemies [i]);
			EnemyHealth eH = newEnemyHealthBar.gameObject.GetComponentInChildren<EnemyHealth> ();
			eH.transform.SetParent(this.transform);
			eH.SetObj (enemies [i]);
		}


	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.I)) {
			this.display = !this.display;
			this.itemInfo.SetActive (this.display);
		}
	}
}
