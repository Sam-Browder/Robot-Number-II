using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject player = GameObject.FindGameObjectWithTag ("Player");

		SkillTimer[] timers = gameObject.GetComponentsInChildren<SkillTimer>();
		timers [0].SetIndex (0);
		timers [1].SetIndex (1);
		timers [2].SetIndex (2);
		timers [3].SetIndex (3);

		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		EnemyHealth eh = gameObject.GetComponentInChildren<EnemyHealth> ();
		eh.SetObj (enemies [1]);

		HealthBar hb = gameObject.GetComponentInChildren<HealthBar> ();
		hb.SetObj (player);
		ShieldBar sb = gameObject.GetComponentInChildren<ShieldBar> ();
		sb.SetObj (player);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
